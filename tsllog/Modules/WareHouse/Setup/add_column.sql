alter table wh_do
add ContainerNo nvarchar(200) null


alter table wh_dodet
add ContainerNo nvarchar(200) null,
	PalletNo nvarchar(200) null

alter table wh_dodet2
add ContainerNo nvarchar(200) null,
    Remark nvarchar(200) null,
	PalletNo nvarchar(200) null,
	Size decimal(10,3) null

alter table wh_ContractDet
add ChgCode nvarchar(200) null,
    Unit nvarchar(200) null

alter table wh_dodet
add GrossWeight decimal(10,3) null,
	NettWeight decimal(10,3) null

alter table wh_dodet2
add GrossWeight decimal(10,3) null,
	NettWeight decimal(10,3) null