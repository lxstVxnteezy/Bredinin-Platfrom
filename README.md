# Bredinin-Platfrom-BackendApi

## Описание
Этот проект представляет собой простой веб-сервис, реализующий RESTful API для управления товарами и категориями товаров. API позволяет выполнять основные CRUD-операции для сущностей "Товар" и "Категория товара".

## Технологии
- **Язык программирования:** C# 
- **Фреймворк:** ASP.NET Core 6
- **ORM:** Entity Framework Core + FluentMigrator
- **База данных:** PostgreSQL
- **Дополнительные библиотеки:** Swagger, FluentValidation

## Эндпоинты API

### 1. ProductCategoryController

#### 1.1 Создание категории товара
- **Метод:** POST
- **URL:** `/api/productCategory/create`
- **Тело запроса:** `ProductCategoryCreateRequest`
- **Ответ:** `ProductCategoryCreateResponse`

#### 1.2 Удаление категории товара
- **Метод:** DELETE
- **URL:** `/api/productCategory/{id}/delete`
- **Параметры:**
  - `id` (Guid) — идентификатор категории товара
- **Ответ:** `204 No Content` при успешном удалении.

#### 1.3 Получение категории товара по ID
- **Метод:** GET
- **URL:** `/api/productCategory/{id}/getById`
- **Параметры:**
  - `id` (Guid) — идентификатор категории товара
- **Ответ:** `InfoProductCategoryResponse`

#### 1.4 Получение всех категорий товара
- **Метод:** GET
- **URL:** `/api/productCategory/getProductCategories`
- **Ответ:** массив `InfoProductCategoryResponse`

#### 1.5 Обновление категории товара
- **Метод:** PUT
- **URL:** `/api/productCategory/{id}/update`
- **Параметры:**
  - `id` (Guid) — идентификатор категории товара
- **Тело запроса:** `UpdateProductCategoryRequest`
- **Ответ:** `UpdateProductCategoryResponse`

---

### 2. ProductController

#### 2.1 Создание товара
- **Метод:** POST
- **URL:** `/api/product/create`
- **Тело запроса:** `ProductCreateRequest`
- **Ответ:** `ProductCreateResponse`

#### 2.2 Удаление товара
- **Метод:** DELETE
- **URL:** `/api/product/{id}/delete`
- **Параметры:**
  - `id` (Guid) — идентификатор товара
- **Ответ:** `204 No Content` при успешном удалении.

#### 2.3 Получение товара по ID
- **Метод:** GET
- **URL:** `/api/product/{id}/getById`
- **Параметры:**
  - `id` (Guid) — идентификатор товара
- **Ответ:** `InfoProductResponse`

#### 2.4 Получение всех товаров
- **Метод:** GET
- **URL:** `/api/product/getProducts`
- **Ответ:** массив `InfoProductResponse`

#### 2.5 Обновление товара
- **Метод:** PUT
- **URL:** `/api/product/{id}/update`
- **Параметры:**
  - `id` (Guid) — идентификатор товара
- **Тело запроса:** `UpdateProductRequest`
- **Ответ:** `UpdateProductResponse`


## Установка и запуск

### Предварительные требования
- [.NET SDK](https://dotnet.microsoft.com/download) ( 6.0 )
- [PostgreSQL](https://www.postgresql.org/download) (15.8 и выше)

### Клонирование репозитория
```bash
git clone https://github.com/lxstVxnteezy/Bredinin-Test.git
```
### Установка зависимостей
Перейдите в папку проекта (src\Bredinin.TestProject\Bredinin.TestProject.Service.Api) и выполните команду:
```bash
dotnet restore
```
### Запуск приложения
Для запуска приложения используйте команду:
```bash
dotnet run
```
### Приложение прослушивается по адресу: https://localhost:7262


## Подключение к базе данных
Для подключения к базе данных PostgreSQL используется следующая строка подключения:

```json
{
  "DefaultConnection": {
    "Host": "localhost",
    "Port": 5432,
    "CommandTimeout": 200,
    "Database": "bredninin",
    "Username": "postgres",
    "Password": "postgres"
  }
}
```
## Список всех ENDPOINTs можно получить по https://localhost:7262/swagger/index.html через Swagger(Полная документация API с помощью Swagger.)

