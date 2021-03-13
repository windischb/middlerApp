﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using JintTsDefinition.Definitions;
using JintTsDefinition.ExtensionMethods;

namespace JintTsDefinition
{
    public class TypeDefinition : IDefinition
    {
        public string Name { get; set; }
        public string Namespace { get; set; }
        public string AccessModifier { get; set; }
        public string Kind { get; set; }

        public bool IsStatic { get; set; }
        public bool IsArray { get; set; }
        public bool IsGeneric { get; set; }

        public string FriendlyName { get; private set; }

        public bool IsNullable { get; set; }
        public TypeDefinition BaseType { get; set; }
        public List<TypeDefinition> ImplementedInterfaces { get; set; }
        public List<TypeDefinition> GenericArguments { get; set; } = new List<TypeDefinition>();

        public List<EnumValueDefinition> EnumValueDefinitions { get; set; } = new List<EnumValueDefinition>();
        public Type RawType { get; set; }

        public List<PropertyDefinition> Properties { get; set; } = new List<PropertyDefinition>();

        public List<ConstructorDefinition> Constructors { get; set; } = new List<ConstructorDefinition>();
        public List<IndexerDefinition> Indexer { get; set; } = new List<IndexerDefinition>();
        public List<MethodDefinition> Methods { get; set; } = new List<MethodDefinition>();

        public override string ToString()
        {

            var str = $"{Name}";
            if (GenericArguments.Any())
            {
                str += $"<{String.Join(", ", GenericArguments.Select(arg => arg.ToString()))}>";
            }

            return str;
        }


        public static TypeDefinition FromType(Type type, IEnumerable<Type> validTypes = null)
        {
            var isNullable = type.IsNullableType();

            if (isNullable)
            {
                type = Nullable.GetUnderlyingType(type);
            }


            if (type?.FullName?.EndsWith("&") == true || type?.FullName?.EndsWith("*") == true)
            {
                type = Type.GetType(type.FullName.TrimEnd("&*".ToCharArray()));
            }

            if (type == null)
            {
                return TypeCache.JsNone;
            }


            if (validTypes != null && !validTypes.Contains(type))
            {
                if (!type.IsGenericType && !type.IsGenericTypeParameter())
                    return TypeCache.JsAny;
            }

            

            var fName = type.GetFriendlyName();
            if (TypeCache.Cache.TryGetValue(fName, out var tDesc))
            {
                return tDesc;
            }

            tDesc = new TypeDefinition
            {
                RawType = type,
                IsNullable = isNullable,
                IsStatic = type.IsStatic(),
                FriendlyName = fName
            };

            TypeCache.Cache.TryAdd(fName, tDesc);

            var tdInfo = type.GetTypeInfo();
            var isGenericTypeParameter = tdInfo.IsGenericTypeParameter();
            tDesc.IsArray = tdInfo.IsArray;
            tDesc.IsGeneric = tdInfo.IsGenericTypeParameter() || tdInfo.IsGenericParameter;

           
            if (tDesc.IsArray)
            {

                var arrElementInfo = tdInfo.GetElementType()?.GetTypeInfo();
                tdInfo = arrElementInfo;
                if (arrElementInfo?.IsGenericTypeParameter() == true)
                {
                    isGenericTypeParameter = true;
                }

            }

            string name = tdInfo.Name;
            name = name.Contains('`') ? name.Remove(name.IndexOf('`')) : name;
            if (!isGenericTypeParameter && !tdInfo.IsGenericParameter)
            {
                tDesc.Namespace = tdInfo.Namespace;
            }

            tDesc.Name = name;

            tDesc.AccessModifier = GetAccessModifier(tdInfo.Attributes);


            if (tdInfo.IsGenericType)
            {
                tDesc.GenericArguments = tdInfo.GenericTypeParameters.Any() ?
                    tdInfo.GenericTypeParameters.Select(t => FromType(t, validTypes)).ToList() :
                    tdInfo.GenericTypeArguments.Select(t => FromType(t, validTypes)).ToList();
                tDesc.Name = $"{tDesc.Name}${tDesc.GenericArguments.Count}";
            }

            

            if (tdInfo.IsClass)
            {
                tDesc.Kind = "class";
            }
            else if (tdInfo.IsInterface)
            {
                tDesc.Kind = "interface";
            }
            else if (tdInfo.IsEnum)
            {
                tDesc.Kind = "enum";
            }
            else if (tdInfo.IsValueType)
            {
                tDesc.Kind = "struct";
            }


            if (type.IsEnum && !tDesc.IsGeneric)
            {


                try
                {
                    var fields = type.GetFields();
                    var names = Enum.GetNames(type);
                    //var values = Enum.GetValues(type);

                    var enumValueDefinitions = new List<EnumValueDefinition>();
                    for (var i = 0; i < names.Length; i++)
                    {
                        var evd = new EnumValueDefinition
                        {
                            Name = names[i],
                            Value = Convert.ChangeType(Enum.Parse(type, names[i]), fields.First().FieldType)
                        };
                        enumValueDefinitions.Add(evd);
                    }

                    tDesc.EnumValueDefinitions = enumValueDefinitions;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
               
            }
            else
            {

                tDesc.Constructors =
                    type.GetConstructors(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                        .Select(ConstructorDefinition.FromConstructorInfo).ToList();

                tDesc.Properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                    .Where(p => !p.IsIndexerProperty())
                    .Select(PropertyDefinition.FromPropertyInfo).ToList();

                tDesc.Indexer = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                    .WhichIsIndexerProperty()
                    .Select(IndexerDefinition.FromPropertyInfo).Where(p => !p.Name.Contains(".")).ToList();

                tDesc.Methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                    .Where(m => !m.IsSpecialName)
                    .Where(m => !m.Name.StartsWith("<"))
                    .Select(MethodDefinition.FromMethodInfo).ToList();

                if (tdInfo.BaseType != null)
                {

                    tDesc.BaseType = FromType(tdInfo.BaseType);
                }

                tDesc.ImplementedInterfaces = tdInfo.GetDirectInterfaces().Select(t => FromType(t, validTypes)).ToList();

            }


           

            return tDesc;
        }

        private static string GetAccessModifier(TypeAttributes attributes)
        {
            if ((attributes & TypeAttributes.Public) == TypeAttributes.Public)
            {
                return "public";
            }

            if ((attributes & TypeAttributes.NestedFamORAssem) == TypeAttributes.NestedFamORAssem)
            {
                return "protected internal";
            }

            if ((attributes & TypeAttributes.NestedFamily) == TypeAttributes.NestedFamily)
            {
                return "protected";
            }

            if ((attributes & TypeAttributes.NestedAssembly) == TypeAttributes.NestedAssembly)
            {
                return "internal";
            }

            if ((attributes & TypeAttributes.NestedFamANDAssem) == TypeAttributes.NestedFamANDAssem)
            {
                return "private protected";
            }

            if ((attributes & TypeAttributes.NestedPrivate) == TypeAttributes.NestedPrivate)
            {
                return "private";
            }

            return null;
        }

      
    }
}