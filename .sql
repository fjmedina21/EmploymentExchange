/****** Script for SelectTopNRows command from SSMS  ******/
select u.Email as 'User', r.Name as 'Role' from users u 
join RoleUser ru on ru.UserId = u.id
join roles r on r.id = ru.RoleId

-- select * from roles
-- select * from users

-- insert into roleuser values ('','')
-- insert into roleuser values ('','')
-- insert into roleuser values ('','')
-- insert into roleuser values ('','')
