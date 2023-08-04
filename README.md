# OnlineStoreAPI

## This is EAV online store API for everything
### This project is based on the EAV pattern. It is a platform where you can place goods of different categories and purposes. You just need to develop the Client.
<hr>

# ITEM API
## 1) Create
### api/item/createitem | POST |
```json
{
  "title": "GIGABYTE GeForce RTX4080 EAGLE OC 16GB",
  "price": 1220,
  "releaseDate": "2023-06-20T05:31:40.623Z",
  "companyId": 1,
  "itemCategoryId": 2,
  "itemProperyValues": [
     {
        "itemPropertyId": 12,
        "value": "NVIDIA® GeForce® RTX 4080"
      },
      {
        "itemPropertyId": 13,
        "value": "2205"
      },
      {
        "itemPropertyId": 14,
        "value": "true"
      },
      {
        "itemPropertyId": 15,
        "value": "16"
      },
      {
        "itemPropertyId": 16,
        "value": "GDDR6X"
      },
      {
        "itemPropertyId": 17,
        "value": "256"
      },
      {
        "itemPropertyId": 18,
        "value": "PCIe 4.0"
      },
      {
        "itemPropertyId": 19,
        "value": "1"
      },
      {
        "itemPropertyId": 20,
        "value": "0"
      },
      {
        "itemPropertyId": 21,
        "value": "0"
      },
      {
        "itemPropertyId": 22,
        "value": "0"
      },
      {
        "itemPropertyId": 23,
        "value": "3"
      },
      {
        "itemPropertyId": 24,
        "value": "0"
      },
      {
        "itemPropertyId": 25,
        "value": "true"
      },
      {
        "itemPropertyId": 26,
        "value": "false"
      },
      {
        "itemPropertyId": 27,
        "value": "false"
      },
      {
        "itemPropertyId": 28,
        "value": "DirectX 12.1"
      },
      {
        "itemPropertyId": 29,
        "value": "Активное охлаждение"
      },
      {
        "itemPropertyId": 30,
        "value": "Четырехслотовый"
      },
      {
        "itemPropertyId": 31,
        "value": "342"
      },
      {
        "itemPropertyId": 32,
        "value": "false"
      },
      {
        "itemPropertyId": 33,
        "value": "многоцветная (RGB)"
      },
      {
        "itemPropertyId": 34,
        "value": "1x 16 pin (12VHPWR)"
      },
      {
        "itemPropertyId": 35,
        "value": "850"
      }
  ]
}
```

# ITEM CATEGORY API
## 1) Create
### api/itemCategory/CreateItemCategory
```json
{
  "id": 0,
  "name": "Блоки питания",
  "categoryId": 1,
  "itemProperties": [
    {
      "id": 0,
      "name": "Мощность",
      "valueType": "int"
    },
    {
      "id": 0,
      "name": "Форм-фактор",
      "valueType": "string"
    },
    {
      "id": 0,
      "name": "Корректор коэффициента мощности (PFC)",
      "valueType": "string"
    },
    {
      "id": 0,
      "name": "Разъемы PCIe 16pin (12+4 12VHPWR)",
      "valueType": "int"
    },
    {
      "id": 0,
      "name": "Разъемы PCIe 8 pin",
      "valueType": "int"
    },
    {
      "id": 0,
      "name": "Разъемы PCIe 6 pin",
      "valueType": "int"
    },
    {
      "id": 0,
      "name": "Разъемы Molex",
      "valueType": "int"
    },
    {
      "id": 0,
      "name": "Разъемы SATA",
      "valueType": "int"
    },
    {
      "id": 0,
      "name": "Система охлаждения",
      "valueType": "string"
    },
    {
      "id": 0,
      "name": "Количество вентиляторов",
      "valueType": "int"
    },
    {
      "id": 0,
      "name": "Размер вентилятора",
      "valueType": "int"
    },
    {
      "id": 0,
      "name": "Сертификат 80 PLUS",
      "valueType": "string"
    },
    {
      "id": 0,
      "name": "Кабельная разводка",
      "valueType": "bool"
    },
    {
      "id": 0,
      "name": "С подсветкой",
      "valueType": "bool"
    }
  ]
}
```
