#!/bin/bash 
url="https://localhost:7270/api"
jwt_token=""

### Auth
curl -X 'POST' \
  "$url/auth/login" \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "email": "admin@gmail.com",
  "password": "zaq1@WSX"
}'

### Orders
curl -X 'GET' \
  "$url/orders" \
  -H 'accept: text/plain' \
  -H "Authorization: Bearer $jwt_token"

curl -X 'GET' \
  "$url/orders/1" \
  -H 'accept: text/plain' \
  -H "Authorization: Bearer $jwt_token"

curl -X 'POST' \
  "$url/orders" \
  -H 'accept: */*' \
  -H "Authorization: Bearer $jwt_token"
  -H 'Content-Type: application/json' \
  -d '{
  "street": "ul. Mickiewicza",
  "city": "Krakau",
  "postalCode": "12-345",
  "country": "Poland",
  "productQuantity": 1,
  "productId": 1,
  "orderStatusId": 1,
  "orderedById": "string"
}'

curl -X 'PUT' \
  "$url/api/orders/1" \
  -H 'accept: */*' \
  -H "Authorization: Bearer $jwt_token"
  -H 'Content-Type: application/json' \
  -d '{
	"id": 0,
	"street": "string",
	"city": "string",
	"postalCode": "string",
	"country": "string",
	"productQuantity": 0,
	"productId": 0,
	"orderStatusId": 0,
	"orderedById": "string"
}'

curl -X 'DELETE' \
  "$url/api/orders/4" \
  -H 'accept: */*' \
  -H "Authorization: Bearer $jwt_token"

### Order Statuses
curl -X 'GET' \
  "$url/orderStatuses" \
  -H 'accept: text/plain' \
  -H "Authorization: Bearer $jwt_token"

curl -X 'GET' \
  "$url/orderStatuses/1" \
  -H 'accept: text/plain' \
  -H "Authorization: Bearer $jwt_token"

curl -X 'POST' \
  "$url/orderStatuses" \
  -H 'accept: */*' \
  -H "Authorization: Bearer $jwt_token"
  -H 'Content-Type: application/json' \
  -d '{
  "name": "Canceled"
}'

curl -X 'PUT' \
  "$url/orderStatuses/1" \
  -H 'accept: */*' \
  -H "Authorization: Bearer $jwt_token"
  -H 'Content-Type: application/json' \
  -d '{
  "id": 1,
  "name": "created"
}'

### Product Categories
curl -X 'GET' \
  "$url/productCategories" \
  -H 'accept: text/plain' \
  -H "Authorization: Bearer $jwt_token"

curl -X 'GET' \
  "$url/productCategories/1" \
  -H 'accept: text/plain' \
  -H "Authorization: Bearer $jwt_token"

curl -X 'POST' \
  "$url/productCategories" \
  -H 'accept: */*' \
  -H "Authorization: Bearer $jwt_token"
  -H 'Content-Type: application/json' \
  -d '{
  "name": "sport"
}'

curl -X 'PUT' \
  "$url/productCategories/1" \
  -H 'accept: */*' \
  -H "Authorization: Bearer $jwt_token"
  -H 'Content-Type: application/json' \
  -d '{
  "id": 1,
  "name": "Games"
}'

### Products
curl -X 'GET' \
  "$url/products" \
  -H 'accept: text/plain' \
  -H "Authorization: Bearer $jwt_token"

curl -X 'GET' \
  "$url/products/1" \
  -H 'accept: text/plain' \
  -H "Authorization: Bearer $jwt_token"

curl -X 'POST' \
  "$url/products" \
  -H 'accept: */*' \
  -H "Authorization: Bearer $jwt_token"
  -H 'Content-Type: application/json' \
  -d '{
  "name": "Ball",
  "description": "Nike ball",
  "quantity": 100,
  "price": 200,
  "productCategoryId": 2
}'

curl -X 'PUT' \
  "$url/products/1" \
  -H 'accept: */*' \
  -H "Authorization: Bearer $jwt_token"
  -H 'Content-Type: application/json' \
  -d '{
  "id": 1,
  "name": "Test",
  "description": "test",
  "quantity": 100,
  "price": 12,
  "productCategoryId": 1
}'

curl -X 'DELETE' \
   "$url/products/3" \
  -H 'accept: */*' \
  -H "Authorization: Bearer $jwt_token"

### Users
curl -X 'GET' \
  "$url/users" \
  -H 'accept: text/plain' \
  -H "Authorization: Bearer $jwt_token"

curl -X 'POST' \
  "$url/users" \
  -H 'accept: */*' \
  -H "Authorization: Bearer $jwt_token"
  -H 'Content-Type: application/json' \
  -d '{
  "userName": "test",
  "email": "test@gmail.com",
  "phoneNumber": "123123123"
}'

curl -X 'GET' \
   "$url/users/test" \
  -H 'accept: text/plain' \
  -H "Authorization: Bearer $jwt_token"

curl -X 'PUT' \
  "$url/users/test" \
  -H 'accept: */*' \
  -H "Authorization: Bearer $jwt_token"
  -H 'Content-Type: application/json' \
  -d '{
  "id": "test",
  "userName": "test",
  "email": "test@gmail.com",
  "phoneNumber": "999888777"
}'

curl -X 'DELETE' \
  "$url/users/test" \
  -H 'accept: */*' \
  -H "Authorization: Bearer $jwt_token"

### User roles
curl -X 'POST' \
  "$url/users/role" \
  -H 'accept: */*' \
  -H "Authorization: Bearer $jwt_token"
  -H 'Content-Type: application/json' \
  -d '{
  "userEmail": "test@gmail.com",
  "roleName": "admin"
}'

curl -X 'DELETE' \
  "$url/users/role" \
  -H 'accept: */*' \
  -H "Authorization: Bearer $jwt_token"
  -H 'Content-Type: application/json' \
  -d '{
  "userEmail": "test@gmail.com",
  "roleName": "admin"
}'

$SHELL