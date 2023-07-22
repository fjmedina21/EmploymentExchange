/****** Script for SelectTopNRows command from SSMS  ******/
select u.id as 'UserId', u.Email as 'User', r.Name as 'Role', r.id as 'RoleId' from users u 
join RoleUser ru on ru.UserId = u.id
join roles r on r.id = ru.RoleId

 select * from Roles
 select * from Users
 select * from RoleUser

-- insert into roleuser values ('USERID','ROLEID')
-- insert into roleuser values ('','')
-- insert into roleuser values ('','')
-- insert into roleuser values ('','')
