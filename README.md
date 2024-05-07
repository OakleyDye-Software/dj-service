# DJ Service  
### Setting up postgres locally  
The fastest way to get the app up and running locally is to spin up a local instance of postgres to run your app. This is most easily accomplished by using Docker. Verify that you have docker desktop installed and running on your machine. Then, navigate to a terminal and execute the following commands:  

- `docker pull postgres`  
- `docker volume create <volume_name>`  
- `docker run --name <some_name> -e POSTGRES_PASSWORD=<your_password> -p 5432:5432 -v <volume_name> -d postgres`  

Once your docker db is up and running, you can access it by running `docker exec -it <some_name> psql -U postgres`.

### Required user secrets  
```json
{
    "Db_Connection_String": "Host={Host_IP};Port=5432;Database=dj;Username={user_name};Password={password};"    
}
```