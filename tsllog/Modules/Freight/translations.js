function config($translateProvider) {

    $translateProvider
        .translations('en', {

            // Define all menu elements
            DASHBOARD: 'Dashboard',
            SEARCH: 'Search for something',
            OpsFreight: 'Ops - Freight',
            SeaImport: 'Sea Import',
            ImpSchedule: 'Imp Schedule',
            TsSchedule: 'T/S Schedule',
            ImportCONSOL: 'Import CONSOL',
            ImportCOLOAD: 'Import COLOAD',
            ImportFCL: 'Import FCL',
            SeaExort: 'Sea Export',
            LCLSchedule: 'LCL Schedule',
            OpsMasterData: 'Ops - Master Data',
            Customer: 'Customer',
            Vendors: 'Vendors',
            Country: 'Country',
            SeaPort: 'Sea Port',
            Currency: 'Currency',
            Sales: 'Sales',
            Term: 'Term',
            General: 'General',
            CargoUom: 'Cargo Uom',
            ContainerType: 'Container Type',
            JobCategory: 'Job Category',
            Commodity: 'Commodity',
            CargoClass: 'Cargo Class',
            Save: 'Save',
            PrintDocuments: 'Print Documents',
            ImportNo: 'Import No',
            ImpRefNo: 'Imp Ref No',
            Vessel: 'Vessel',
            Voyage: 'Voyage',
            JobCompleteInd: 'Job Complete Ind',
            PortLoadId: 'Port Load Id',
            PortDiscId: 'Port Disc Id',
            DropYN: 'Drop(Y,N)',
            AgentId: 'Agent Id',
            clear: 'clear',
            OceanBL: 'Ocean BL',
            CarrierBkgRef: 'Carrier Bkg Ref',
            NVOBL: 'NVO BL',
            Eta: 'Eta',
            Etd: 'Etd',
            Warehouse: 'Warehouse',
            Total: 'Total',
            TotalM3: 'Total M3',
            TotalWt: 'TotalWt',
            TotalPkgs: 'TotalPkgs',
            PkgsType: 'Pkgs Type',
            NvoccAgent: 'Nvocc Agent',
            CarrierAgent: 'Carrier Agent',
            RefCurrency: 'Ref Currency',
            TsExRate: 'T/S ExRate',
            Remark: 'Remark',
            Creation: 'Creation',
            LastUpdated: 'Last Updated',
            JobStatus: 'Job Status',
            TransportatinInfo: 'Transportatin Info',
            UENNo: 'UEN No',
            Attention: 'Attention',
            Fax: 'Fax',
            DriverName: 'Driver Name',
            DriverMobile: 'Driver Mobile',
            DriverLicense: 'Driver License',
            VehicleNo: 'Vehicle No',
            VehicleType: 'Vehicle Type',
            DriverRemark: 'Driver Remark',
            TruckTo: 'Truck To',
            Date: 'Date',
            Time: 'Time',
            SENTTO: 'SENT TO',
            StuffUnstuffBy: 'Stuff Unstuff By',
            Shippingcoload: 'Shipping coload',
            Person: 'Person',
            Telephone: 'Telephone',
            Remarks: 'Remarks',
            Weight: 'Weight',
            Volume: 'Volume',
            Qty: 'Qty',
            PkgsType: 'PkgsType',
            AddInvoice: 'AddInvoice',
            AddCN: 'AddCN',
            AddDN: 'AddDN',
            ChangeVesVoy: 'Change Ves/Voy',
            VesVoy: 'Ves/Voy',
            AgImpRate: 'Ag/Imp Rate',
            AgRates: 'Ag Rates',
            TotAgRate: 'Tot Ag. Rate',
            ImpRates: 'Imp Rates',
            TotImRate: 'Tot. Im Rate',
            AllHouseJobs: 'All House Job(s)',
            Savechanges: 'Save changes',
            Cancelchanges: 'Cancel changes',
            Upload: 'Upload',
            EMPTY_RETURN_LOADED_SENDING: 'EMPTY RETURN/LOADED SENDING',
            Stuff_Unstuff_By: 'Stuff/Unstuff By',
            Shippingcoload: 'Shipping coload',
            AddNew: 'Add New',
            AutoDN: 'Auto DN',
                // Define some custom text

        })
        .translations('zh', {

            // Define all menu elements
            DASHBOARD: '控制面板',
            SEARCH: '查找',
            OpsFreight: '业务 - 海运空运',
            SeaImport: '海运进口',
            ImpSchedule: '进口计划',
            TsSchedule: '转口计划',
            ImportCONSOL: '进口整箱',
            ImportCOLOAD: '进口散拼',
            ImportFCL: '进口整箱',
            SeaExort: '海运出口',
            LCLSchedule: '拼箱船期',
            OpsMasterData: '业务 - 基础数据',
            Customer: '客户列表',
            Vendors: '供应商列表',
            Country: '国家列表',
            SeaPort: '海港列表',
            Currency:'货币',
            Sales: '销售团队',
            Term: '财务条款',
            General: '通用代码',
            CargoUom: '货物种类',
            ContainerType: '集装箱分类',
            JobCategory: '工作分类',
            Commodity: '货物种类',
            CargoClass: '货物级别',
            Save: '保存',
            PrintDocuments: '打印',
            ImportNo: '单号',
        });

    $translateProvider.preferredLanguage('en');

}

angular
    .module('starter')
    .config(config)
