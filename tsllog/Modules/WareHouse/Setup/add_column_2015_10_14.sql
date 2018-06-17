alter table wh_do
add UseTransport nvarchar(200) null,
    TransportStatus nvarchar(200)null,
	TransportStart datetime null,
	TransportEnd datetime null

alter table wh_do
add UseFreight nvarchar(200) null,
    FreightStatus nvarchar(200)null