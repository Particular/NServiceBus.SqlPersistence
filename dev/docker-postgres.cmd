@docker run -d --name PostgresDb -v my_dbdata:/var/lib/postgresql/data -p 54320:5432 -e POSTGRES_PASSWORD=super-secret-password postgres:11