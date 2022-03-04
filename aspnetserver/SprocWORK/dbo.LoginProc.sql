CREATE PROCEDURE [dbo].[Procedure]
	@emailAddr varchar,
	@password varchar
AS
	SELECT email, Password 
	FROM Users
	WHERE EXISTS(
		SELECT email, Password 
		FROM Users 
		WHERE email=@emailAddr AND Password=@password)
	AND email=@emailAddr AND Password=@password;
RETURN 0
