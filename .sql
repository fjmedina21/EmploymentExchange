/****** Script for SelectTopNRows command from SSMS  ******/
select u.Email, r.Name as 'Role' from users u 
join RoleUser ru on ru.UserId = u.id
join roles r on r.id = ru.RoleId