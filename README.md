# Housing Repairs Online Authentication
Simplifies setup of JSON Web Tokens (JWT) authentication by:

- providing extension methods for 
  - setup (on `IServiceCollection`), see `ServiceCollectionExtensions`
  - token retrieval (on `HttpClient`), see `AuthenticationTokenHelper`
- an authentication controller that will provide an authentication endpoint
- abstracts JWT creation

## Using in a web API project
1. Add the nuget.
2. Add authentication
   1. In `ConfigureServices` add the following line, ensuring to change the parameter to an appropriate issuer that will be added to the JWT:
   ```
   services.AddHousingRepairsOnlineAuthentication("<REPLACE WITH APPROPRIATE ISSUER");
   ```
   2. In `Configure` add the following line
   ```
   app.UseAuthentication();
   ```
   3. Also in `Configure`, add call `RequireAuthorization` to lock down all endpoints
   ```
   app.UseEndpoints(endpoints => {
       endpoints.MapControllers().RequireAuthorization();
   });
   ```
3. Optionally, if using Swagger, in `ConfigureServices` within `services.AddSwaggerGen` add `c.AddJwtSecurityScheme();`
   ```
   services.AddSwaggerGen(c =>
   {
       c.SwaggerDoc("v1", new OpenApiInfo { Title = "HousingRepairsOnlineApi", Version = "v1" });
       c.AddJwtSecurityScheme();
   });
   ```
   This will allow setting a JWT authentication token via the Swagger web UI.

After following the steps above, the following describes how to authenticate and make requests.
It's advisable to copy the below to the documentation of consuming projects.

## Authentication
Requests to the API require authentication.
The API implements JSON Web Tokens (JWT) for authentication.

A unique, secret identifier is required to generate a JWT.
This should be set in an `AUTHENTICATION_IDENTIFIER` environment variable which will be consumed during startup.

A JWT can be generated using a POST request to the `Authentication` endpoint, i.e.
```http request
POST https://localhost:5001/Authentication?identifier=<AUTHENTICATION_IDENTIFIER>
```
The body of the response will contain a JWT which will expire after 1 minute.

All other requests require a valid JWT to be sent in the `Authorization` header with a value of
`Bearer <JWT TOKEN>`, i.e.
```http request
GET https://localhost:5001/Addresses?postcode=1
Authorization: Bearer <JWT TOKEN>
```
