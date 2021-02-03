Create Table NetworkEvents (
	ID int Identity (1,1),
	Event_ID int Not Null,
	Event_DT datetime Not Null,
	Switch_IP nvarchar (50) Not Null,
	Port_Id int Not Null,
	Device_Mac nvarchar(50),
	Remarks Nvarchar(max)
)

GO

Insert Into NetworkEvents Values 

(1001, GetDate(), '1.1.1.1', 12, 'AABBCC000001', 'New device was added to port 12 of switch 1.1.1.1'),
(1001, GetDate(),  '1.1.1.1', 11, 'AABBCC000009', 'New device was added to port 12 of switch 1.1.1.1'),
(1003, GetDate(),  '192.168.1.1', 48, NULL, 'Port 48 of switch 192.168.1.1 was disabled'),
(1002, GetDate(),  '1.1.1.1', 12, NULL, 'Device was removed from port 12 of switch 1.1.1.1'),
(1001, GetDate(),  '192.168.1.1', 47, 'AABBCC000001', 'New device was added to port 47 of switch 192.168.1.1')GOCreate Proc sp_GetEvents 	@EventID int = NullasBegin	Select ID, Event_ID, Event_DT, Switch_IP, Port_Id, ISNULL (Device_Mac,'') as Device_Mac, Remarks From NetworkEventsEnd