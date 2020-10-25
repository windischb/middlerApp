!function(){function t(t,n){if(!(t instanceof n))throw new TypeError("Cannot call a class as a function")}function n(t,n){for(var r=0;r<n.length;r++){var o=n[r];o.enumerable=o.enumerable||!1,o.configurable=!0,"value"in o&&(o.writable=!0),Object.defineProperty(t,o.key,o)}}function r(t,r,o){return r&&n(t.prototype,r),o&&n(t,o),t}(window.webpackJsonp=window.webpackJsonp||[]).push([[6],{pGMH:function(n,o,e){"use strict";e.r(o),e.d(o,"FirstSetupModule",(function(){return C}));var i,u=e("2kYt"),s=e("nIj0"),c=e("sEIs"),a=e("EM62"),f=e("ZF+8"),p=e("vobO"),b=((i=function(){function n(r,o,e){t(this,n),this.http=r,this.router=o,this.route=e}return r(n,[{key:"GetSetupViewModel",value:function(){return this.http.get("/api/first-setup")}},{key:"SubmitFirstSetupViewModel",value:function(t){return this.http.post("/api/first-setup",t)}}]),n}()).\u0275fac=function(t){return new(t||i)(a.Xb(p.c),a.Xb(c.i),a.Xb(c.a))},i.\u0275prov=a.Jb({token:i,factory:i.\u0275fac}),i),m=e("PlhU"),l=e("Mr8s"),h=e("mybp"),g=e("1spV"),d=e("duTG"),S=e("ZTwM");function w(t,n){if(1&t){var r=a.Ub();a.Tb(0,"form",1),a.bc("ngSubmit",(function(){return a.vc(r),a.ec().Submit()})),a.Tb(1,"div",2),a.Tb(2,"nz-form-item"),a.Tb(3,"nz-form-control",3),a.Tb(4,"nz-input-group",4),a.Ob(5,"input",5),a.Sb(),a.Sb(),a.Sb(),a.Tb(6,"nz-form-item"),a.Tb(7,"nz-form-control",6),a.Tb(8,"nz-input-group",7),a.Ob(9,"input",8),a.Sb(),a.Sb(),a.Sb(),a.Tb(10,"nz-form-item"),a.Tb(11,"nz-form-control",6),a.Tb(12,"nz-input-group",7),a.Ob(13,"input",9),a.Sb(),a.Sb(),a.Sb(),a.Tb(14,"button",10),a.Fc(15,"Submit"),a.Sb(),a.Sb(),a.Sb()}if(2&t){var o=a.ec();a.lc("formGroup",o.form),a.zb(14),a.lc("nzType","primary")}}var v,y,z=((v=function(){function n(r,o,e,i,u){var s=this;t(this,n),this.uiService=r,this.setupApi=o,this.router=e,this.fb=i,this.cref=u,this.errors=[],console.log("constructor"),r.Set((function(t){t.Header.Title="First Setup",t.Content.Scrollable=!1,t.Sidebar.Hide=!0})),this.setupApi.GetSetupViewModel().subscribe((function(t){console.log(t),t.AdminUserExists?s.router.navigate(["/"]):s.createForm()}))}return r(n,[{key:"createForm",value:function(){this.form=this.fb.group({Username:["",[s.q.required,s.q.minLength(5)]],Password:["",[s.q.required,s.q.minLength(8)]],ConfirmPassword:["",[s.q.required,s.q.minLength(8)]]}),console.log(this.form),this.cref.detectChanges()}},{key:"Submit",value:function(){var t=this;if(this.form.valid){var n=this.form.value;this.errors=[],n.RedirectUri=location.origin,this.setupApi.SubmitFirstSetupViewModel(n).subscribe((function(n){t.router.navigate(["/"])}),(function(n){t.errors=n.error}))}}}]),n}()).\u0275fac=function(t){return new(t||v)(a.Nb(f.a),a.Nb(b),a.Nb(c.i),a.Nb(s.e),a.Nb(a.h))},v.\u0275cmp=a.Hb({type:v,selectors:[["ng-component"]],decls:1,vars:1,consts:[["nz-form","","class","login-form",3,"formGroup","ngSubmit",4,"ngIf"],["nz-form","",1,"login-form",3,"formGroup","ngSubmit"],[2,"width","400px"],["nzErrorTip","Please input your username!"],["nzPrefixIcon","user"],["type","text","nz-input","","formControlName","Username","placeholder","Username"],["nzErrorTip","Please input your Password!"],["nzPrefixIcon","lock"],["type","password","nz-input","","formControlName","Password","placeholder","Password"],["type","password","nz-input","","formControlName","ConfirmPassword","placeholder","Confirm Password"],["nz-button","",1,"login-form-button","login-form-margin",3,"nzType"]],template:function(t,n){1&t&&a.Dc(0,w,16,2,"form",0),2&t&&a.lc("ngIf",n.form)},directives:[u.o,s.r,s.n,m.b,s.h,l.c,m.c,l.a,m.a,h.c,g.a,h.b,s.d,s.m,s.g,d.a,S.a],styles:[".login-form[_ngcontent-%COMP%]{margin:100px auto}.login-form-margin[_ngcontent-%COMP%]{margin-bottom:16px}.login-form-forgot[_ngcontent-%COMP%]{float:right}.login-form-button[_ngcontent-%COMP%]{width:100%}"]}),v),T=e("YPwk"),P=[{path:"",component:z}],C=((y=function n(){t(this,n)}).\u0275mod=a.Lb({type:y}),y.\u0275inj=a.Kb({factory:function(t){return new(t||y)},providers:[b],imports:[[u.c,c.l.forChild(P),s.p,T.a]]}),y)}}])}();