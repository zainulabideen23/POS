I've started this project as "Modular Monolith using Clean Architecture".
Provided you the the basic folder structure for prduct API, which we're gonna implement now.
Take the inspiration from the existing folder structure and implement the Identity module in a similar way. Make sure to adhere to the principles of Clean Architecture, keeping the layers separate and ensuring that dependencies flow inwards.


Now the actual implementation roadmap

This is the part I would follow very strictly.

Phase 0 — Architecture & GitHub
Both

Create:

POS.sln
GitHub repository
GitHub Project
Issues
README
.gitignore
Branch protection
CI

Agree on:

Naming
Folder structure
DTO conventions
Entity conventions
API naming
Exception format
Response format
Commit format
Branch format


Phase 1 — Product API foundation
Shoaib

Implement Product module.

POS.Product.Domain
POS.Product.Application
POS.Product.Infrastructure
POS.ProductAPI

Start with:

Category
Brand
Unit
Product
ProductVariant
ProductBarcode

Don't build the entire system simultaneously.

Product API becomes your reference implementation.

Phase 2 — Infrastructure foundation
Zain

At the same time, Zain works on the shared infrastructure:

Database configuration
Authentication
JWT
Global exception handling
Logging
SharedKernel
API conventions
Testing infrastructure

But don't let SharedKernel become a dumping ground.

Phase 3 — Product API complete

Shoaib finishes:

Create
Read
Update
Delete
Search
Pagination
Filtering
Validation
Authorization

Then both developers review the architecture.

Ask:

Is the Domain clean?
Is Application clean?
Is Infrastructure isolated?
Is API thin?
Are dependencies correct?

Only after this is solid do we replicate the pattern.

Phase 4 — Inventory API
Shoaib
POS.Inventory.Domain
POS.Inventory.Application
POS.Inventory.Infrastructure
POS.InventoryAPI

Implement:

Warehouse
InventoryStock
InventoryTransaction

Then:

Opening Stock
Stock Adjustment
Stock Availability
Low Stock
Phase 5 — Customer API
Shoaib
Customer
CustomerAPI

Implement:

Create
Update
Delete
Search
Get
Phase 6 — Supplier API
Shoaib
Supplier
SupplierAPI
Phase 7 — Purchase API
Shoaib
Purchase
PurchaseItem
PurchaseAPI

Workflow:

Create Purchase
       ↓
Add Items
       ↓
Confirm Purchase
       ↓
Inventory increases

This is where you implement your first important cross-module boundary.

Phase 8 — Identity
Zain
IdentityAPI

Implement:

Registration
Login
JWT
Roles
Authorization
Tenant
Branch
User access

Then secure all APIs.

Phase 9 — Sales API
Zain
Sales.Domain
Sales.Application
Sales.Infrastructure
SalesAPI

Implement:

Sale
SaleItem

Workflow:

Customer
   ↓
Scan Product
   ↓
Sale Items
   ↓
Calculate
   ↓
Checkout
Phase 10 — Payment API
Zain

Implement:

PaymentMethod
Payment

Support initially:

Cash
Card
Bank Transfer
Credit

Don't build JazzCash/Easypaisa integrations in MVP.

Phase 11 — Sales + Inventory integration

This is one of the most important stages.

SalesAPI
    │
    │ stock operation
    ▼
Inventory module
    │
    ├── Check stock
    ├── Deduct stock
    └── Create transaction

And:

Sale Return
    ↓
Inventory
    ↓
Restore stock

Neither module accesses the other's tables directly.

Phase 12 — Reporting
Zain

Build read-only reporting.

Sales
Purchases
Inventory
Customers

Reports:

Today's Sales
Monthly Sales
Top Products
Low Stock
Sales by Payment Method
Purchase Summary

Don't create redundant report tables unless profiling later proves you need them.

Phase 13 — Testing

Both.

Unit tests

Test business logic:

Sale total calculation
Discount calculation
Stock validation
Purchase calculation
Product validation
Integration tests

Test:

API
 ↓
Application
 ↓
Infrastructure
 ↓
Database

Examples:

POST /api/products
POST /api/purchases
POST /api/sales
Phase 14 — MVP hardening

Before calling MVP finished:

Authentication
Authorization
Tenant isolation
Validation
Exception handling
Logging
Pagination
Filtering
Searching
Indexes
Database constraints
Concurrency
Transaction handling
API documentation
Testing