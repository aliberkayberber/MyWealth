# MyWealt

We start by registering on the website. Then we log in and a special token is generated for the user. User and admin access are different, admin can do everything but users can only do transactions related to themselves. After logging in, you have access to all stocks registered in the database. We can see all the features of these stocks separately. The user can also search by stock symbols. The user can add, remove or make changes to the stocks they want to their portfolio. We can comment on the stocks as much as we want. We can delete or change the comments we make. We can see the comments we make and the stocks we own on a single page. The admin can take the site under maintenance or add new stocks to the database if they want.

## Prerequisites

- .NET 8 SDK
- SQL Server
- Visual Studio or any C# compatible IDE

## Features

- User login with JWT authentication
- Swagger for API documentation
- Layered architect
- Entity Framework Code First
- Authentication
- Authorization
- ASP.NET Core Custom Identity
- Middleware 
- Action Filter
- Model validation
- Dependency Injection
- Data Protection
- Global exception handling
- Pagination

## Endpoints

- `POST /api/auth/login`: User login endpoint
- `POST /api/auth/register`: User register endpoint
- `POST /api/comments`: User creates comment
- `GET /api/comments/userallcomments`: User sees all comments
- `PATCH /api/comments/{id}/update`: User edits a comment he/she made
- `DELETE /api/comments/{id}`:User deletes a comment with its id
- `GET /api/portfolios/username`: User sees the stocks in his/her portfolio
- `POST /api/portfolios/username`: User adds a stock to his/her portfolio.
- `DELETE /api/portfolios`: User deletes a stock from the portfolio.
- `PATCH /api/portfolios`:User exchanges one stock for another from his portfolio
- `PUT /api/portfolios`:Replaces all shares given by the user with all shares in the portfolio
- `GET /api/stocks/{pagenumber}/{pagesize}`: User sees all shares page by page
- `GET /api/stocks/{id}`: Views the details of the idli share given by the user.
- `GET /api/stocks/search`: User searches by symbol.
- `DELETE /api/stocks/{id}`: Deletes the share given by the user(Admin)
- `POST /api/stocks`: Admin adds stocks between 6am and 12pm
- `PATCH /api/stocks/{id}/purchase`: Admin changes the purchase of stock
- `PATCH /api/settings`: Admin puts the site into maintenance mode


### Installing

1. **Clone the Repository**
   ```bash
   git clone https://github.com/aliberkayberber/MyWealth
   ```
2. **Configure the database connection**

```bash
    
   - Update the connection string in `appsettings.json`:
    
     "ConnectionStrings": {
     "Default": "Server=your_server;Database=your_database;trusted_connection=true;TrustServerCertificate=true;"
       },
     "Jwt": {
       "Key": "your_secret_key",
       "Issuer": "your_issuer",
       "Audience": "your_audience"
       }
```
3. **Run database migrations**:
  
    ```bash
    
    dotnet ef database update
    ```
4. **Run the application**:
   
   ```bash
   
    dotnet run
    ```

    

End with an example of getting some data out of the system or using it
for a little demo

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Contact

For any questions or support, please email aliberkayberber@gmail.com