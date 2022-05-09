# ThisBistro API ([Demo](https://thisbistro.com/))
They reset every 20 minutes, so feel free to test them!

<img src="https://projectcode9.com/images/thisbistro1.jpg" width="270"> <img src="https://projectcode9.com/images/thisbistro2.jpg" width="270"> <img src="https://projectcode9.com/images/thisbistro3.jpg" width="270">

A restaurant website developed with ASP.NET Core 6.0 Web API, Entity Framework 6 and PostgreSQL 14.  
The frontend part is developed with Angular 13 and Bootstrap 5, and can be found [here](https://github.com/projjk/ThisBistro-ng)

# Security
- HTTPS Connection
- Cross-Origin Resource Sharing (CORS)

# Endpoints
## For Customers
### CartController
- GET api/cart
- POST api/cart
- GET api/cart/{id}
- PUT api/cart/{id}
- DELETE api/cart/{id}
### CategoryController
- GET api/category
- GET api/category/{id}
### MenuController
- GET api/menu
- GET api/menu/{id}
### OrderController
- GET api/order
- POST api/order
- GET api/order/{id}

## 
## For Admins
### Manager/CategoryController
- GET api/manager/category
- POST api/manager/category
- GET api/manager/category/{id}
- PUT api/manager/category/{id}
- DELETE api/manager/category/{id}
### Manager/ConfigController
- GET api/manager/config
- PUT api/manager/config
### Manager/MenuController
- GET api/manager/menu
- POST api/manager/menu
- GET api/manager/menu/{id}
- PUT api/manager/menu/{id}
- DELETE api/manager/menu/{id}
### Manager/OrderController
- GET api/manager/order
- GET api/manager/order/{id}
- PUT api/manager/order/{id}
- DELETE api/manager/order/{id}

---

I learned how Angular works and how it communicates with the backend APIs developed with ASP.NET Core Web API. While working on this project, I also learned about RxJs, different HTTP methods and status codes, various CSS frameworks, containerizing with Docker, and AWS services for deployment.

The backend is also designed to accomodate further implementation of Swagger and Auth0.
