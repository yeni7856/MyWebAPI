CREATE TABLE [dbo].[PointLogs]
(
	Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,	--일련번호
	UserId INT NOT NULL,						--사용자 ID
	UserName NVARCHAR(25),						--사용자 이름
	TotalPoint INT Default(0),					--종합포인트
	Created DatetimeOffset(7) Default(GetDate()) --등록 일시
)
