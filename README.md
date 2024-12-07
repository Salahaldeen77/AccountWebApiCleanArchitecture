# AccountWebApiCleanArchitecture

مشروع أدارة الحسابات باستخدام ASP.NET Core Web API وبنية النظافة (Clean Architecture).

## الوصف

هذا المشروع يهدف إلى توفير واجهة برمجة تطبيقات (API) لحسابات المستخدمين وادارة عمليات السحب والايداع والتحويل بين الحسابات وادارة الحسابات باستخدام بنية النظافة Clean Architecture في ASP.NET Core، مع استخدام نهج الكود أولًا (Code First).

## المكونات

1. **نمط تصميم CQRS**
2. **نمط تصميم المستودع العام (Generic Repository)**
3. **نظام الترقيم الصفحات (Pagination)**
4. **توطين البيانات والاستجابات**
5. **التحقق باستخدام Fluent Validations**
6. **التكوينات باستخدام Data Annotations**
7. **التكوينات باستخدام Fluent API**
8. **نقاط النهاية للعمليات**
9. **السماح لـ CORS**
10. **استخدام الهوية (Identity)**
11. **إضافة المصادقة**
12. **إضافة JWT Token وSwaggerGen**
13. **التفويض (الأدوار، المطالبات)**
14. **خدمات مثل إرسال البريد الإلكتروني، تحميل الصور**
15. **فلاتر**
16. **عمليات قاعدة البيانات (العروض، الإجراءات، الوظائف) نقطة النهاية**
17. **تسجيل الأحداث (Logs)**
18. **اختبارات الوحدة (XUnit)**

## المتطلبات

- .NET Core SDK
- قاعدة بيانات مدعومة

## كيفية البدء

1. **استنساخ المستودع:**

   ```bash
   git clone https://github.com/Salahaldeen77/AccountWebApiCleanArchitecture.git
   ```

2. **الدخول إلى مجلد المشروع:**

   ```bash
   cd AccountWebApiCleanArchitecture
   ```

3. **استعادة الحزم:**

   ```bash
   dotnet restore
   ```

4. **بناء المشروع:**

   ```bash
   dotnet build
   ```

5. **تشغيل التطبيق:**

   ```bash
   dotnet run
   ```

## بنية المشروع

- `AccountWeb.Api`: يحتوي على منطق API.
- `AccountWeb.Core`: يحتوي على النماذج والمنطق الأساسي.
- `AccountWeb.Data`: يحتوي على منطق الوصول إلى البيانات.
- `AccountWeb.Infrastructure`: يحتوي على الخدمات والبنية الأساسية.
- `AccountWeb.Service`: يحتوي على الخدمات المنطقية للتطبيق.

## المساهمة

مرحب بالمساهمات! الرجاء فتح "issues" للمناقشة قبل تقديم "pull requests".



# AccountSysWebApiInCleanArchitecture

## Description
AccountProject Using Asp.net Core Web Api Using Clean Architecture Based On Code First

### Components

1.CQRS Design Pattern

2.Generic(Repository) Design Pattern

3.Pagination Schema

4.Localizations Of Data And Responses

5.Fluent Validations 

6.Configurations Using Data Annotations

7.Configurations using Fluent API

8.EndPoints Of Operations

9.Allow CORS

10.Using Identity

11.Added Authentication

12.Added JWT Token And SwaggerGen

13.Authorizations(Roles,Claims)

14.Service Like Send (Email,Upload Image)

15.Filters

16.Database Operations(Views,Procedures,Functions) Endpoint

17.Logs

18.Unit Test (XUnit)

