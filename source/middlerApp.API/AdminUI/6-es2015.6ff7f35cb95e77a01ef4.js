(window.webpackJsonp=window.webpackJsonp||[]).push([[6],{pGMH:function(t,r,o){"use strict";o.r(r),o.d(r,"FirstSetupModule",(function(){return z}));var n=o("2kYt"),e=o("nIj0"),i=o("sEIs"),s=o("EM62"),u=o("ZF+8"),c=o("vobO");let p=(()=>{class t{constructor(t,r,o){this.http=t,this.router=r,this.route=o}GetSetupViewModel(){return this.http.get("/api/first-setup")}SubmitFirstSetupViewModel(t){return this.http.post("/api/first-setup",t)}}return t.\u0275fac=function(r){return new(r||t)(s.Xb(c.c),s.Xb(i.i),s.Xb(i.a))},t.\u0275prov=s.Jb({token:t,factory:t.\u0275fac}),t})();var m=o("PlhU"),b=o("Mr8s"),a=o("mybp"),f=o("1spV"),l=o("duTG"),h=o("ZTwM");function d(t,r){if(1&t){const t=s.Ub();s.Tb(0,"form",1),s.bc("ngSubmit",(function(){return s.vc(t),s.ec().Submit()})),s.Tb(1,"div",2),s.Tb(2,"nz-form-item"),s.Tb(3,"nz-form-control",3),s.Tb(4,"nz-input-group",4),s.Ob(5,"input",5),s.Sb(),s.Sb(),s.Sb(),s.Tb(6,"nz-form-item"),s.Tb(7,"nz-form-control",6),s.Tb(8,"nz-input-group",7),s.Ob(9,"input",8),s.Sb(),s.Sb(),s.Sb(),s.Tb(10,"nz-form-item"),s.Tb(11,"nz-form-control",6),s.Tb(12,"nz-input-group",7),s.Ob(13,"input",9),s.Sb(),s.Sb(),s.Sb(),s.Tb(14,"button",10),s.Fc(15,"Submit"),s.Sb(),s.Sb(),s.Sb()}if(2&t){const t=s.ec();s.lc("formGroup",t.form),s.zb(14),s.lc("nzType","primary")}}let g=(()=>{class t{constructor(t,r,o,n,e){this.uiService=t,this.setupApi=r,this.router=o,this.fb=n,this.cref=e,this.errors=[],console.log("constructor"),t.Set(t=>{t.Header.Title="First Setup",t.Content.Scrollable=!1,t.Sidebar.Hide=!0}),this.setupApi.GetSetupViewModel().subscribe(t=>{console.log(t),t.AdminUserExists?this.router.navigate(["/"]):this.createForm()})}createForm(){this.form=this.fb.group({Username:["",[e.q.required,e.q.minLength(5)]],Password:["",[e.q.required,e.q.minLength(8)]],ConfirmPassword:["",[e.q.required,e.q.minLength(8)]]}),console.log(this.form),this.cref.detectChanges()}Submit(){if(!this.form.valid)return;let t=this.form.value;this.errors=[],t.RedirectUri=location.origin,this.setupApi.SubmitFirstSetupViewModel(t).subscribe(t=>{this.router.navigate(["/"])},t=>{this.errors=t.error})}}return t.\u0275fac=function(r){return new(r||t)(s.Nb(u.a),s.Nb(p),s.Nb(i.i),s.Nb(e.e),s.Nb(s.h))},t.\u0275cmp=s.Hb({type:t,selectors:[["ng-component"]],decls:1,vars:1,consts:[["nz-form","","class","login-form",3,"formGroup","ngSubmit",4,"ngIf"],["nz-form","",1,"login-form",3,"formGroup","ngSubmit"],[2,"width","400px"],["nzErrorTip","Please input your username!"],["nzPrefixIcon","user"],["type","text","nz-input","","formControlName","Username","placeholder","Username"],["nzErrorTip","Please input your Password!"],["nzPrefixIcon","lock"],["type","password","nz-input","","formControlName","Password","placeholder","Password"],["type","password","nz-input","","formControlName","ConfirmPassword","placeholder","Confirm Password"],["nz-button","",1,"login-form-button","login-form-margin",3,"nzType"]],template:function(t,r){1&t&&s.Dc(0,d,16,2,"form",0),2&t&&s.lc("ngIf",r.form)},directives:[n.o,e.r,e.n,m.b,e.h,b.c,m.c,b.a,m.a,a.c,f.a,a.b,e.d,e.m,e.g,l.a,h.a],styles:[".login-form[_ngcontent-%COMP%]{margin:100px auto}.login-form-margin[_ngcontent-%COMP%]{margin-bottom:16px}.login-form-forgot[_ngcontent-%COMP%]{float:right}.login-form-button[_ngcontent-%COMP%]{width:100%}"]}),t})();var S=o("YPwk");const w=[{path:"",component:g}];let z=(()=>{class t{}return t.\u0275mod=s.Lb({type:t}),t.\u0275inj=s.Kb({factory:function(r){return new(r||t)},providers:[p],imports:[[n.c,i.l.forChild(w),e.p,S.a]]}),t})()}}]);