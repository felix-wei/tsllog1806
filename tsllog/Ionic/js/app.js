// Ionic Starter App

// angular.module is a global place for creating, registering and retrieving Angular modules
// 'starter' is the name of this angular module example (also set in a <body> attribute in index.html)
// the 2nd parameter is an array of 'requires'
// 'starter.controllers' is found in controllers.js
angular.module('starter', ['ionic', 'starter.controllers', 'starter.services','starter.filters'])

.run(function($ionicPlatform) {
  $ionicPlatform.ready(function() {
    // Hide the accessory bar by default (remove this to show the accessory bar above the keyboard
    // for form inputs)
    if(window.cordova && window.cordova.plugins.Keyboard) {
      cordova.plugins.Keyboard.hideKeyboardAccessoryBar(true);
    }
    if(window.StatusBar) {
      // org.apache.cordova.statusbar required
      StatusBar.styleDefault();
    }
  });
})

.config(function($stateProvider, $urlRouterProvider) {
  $stateProvider

    .state('menu', {
        url: "/menu",
      abstract: true,
      templateUrl: "templates/menu.html",
      controller: 'Ctrl_Menu'
    })

    .state('menu.login', {
        url: "/login",
        views: {
            'menuContent': {
                templateUrl: "templates/menu_login.html",
                controller: 'Ctrl_Login'
            }
        }
    })
    .state('menu.user', {
        url: "/user",
        views: {
            'menuContent': {
                templateUrl: "templates/menu_user.html",
                controller: 'Ctrl_User'
            }
        }
    })

    .state('menu.schedule', {
        url: "/schedule",
        views: {
            'menuContent': {
                templateUrl: "templates/menu_schedule.html",
                controller: 'Ctrl_Schedule'
            }
        }
    })

    .state('menu.scheduletrip', {
        url: "/scheduletrip/:No",
        views: {
            'menuContent': {
                templateUrl: "templates/menu_schedule_trip.html",
                controller: 'Ctrl_ScheduleTrip'
            }
        }
    })

    .state('menu.schedule1', {
        url: "/schedule1",
        views: {
            'menuContent': {
                templateUrl: "templates/menu_schedule1.html",
                controller: 'Ctrl_Schedule1'
            }
        }
    })

    .state('menu.joblist', {
        url: "/joblist",
        views: {
            'menuContent': {
                templateUrl: "templates/menu_job_list.html",
                controller: 'Ctrl_JobList'
            }
        }
    })

    .state('menu.jobdetail', {
        url: "/jobdetail/:No",
        views: {
            'menuContent': {
                templateUrl: "templates/menu_job_detail.html",
                controller: 'Ctrl_JobDetail'
            }
        }
    })

    .state('menu.contactlist', {
        url: "/contactlist",
        views: {
            'menuContent': {
                templateUrl: "templates/menu_contact_list.html",
                controller: 'Ctrl_ContactList'
            }
        }
    })

    .state('menu.contactdetail', {
        url: "/contactdetail/:Index",
        views: {
            'menuContent': {
                templateUrl: "templates/menu_contact_detail.html",
                controller: 'Ctrl_ContactDetail'
            }
        }
    })

    .state('menu.contactdetail1', {
        url: "/contactdetail1/:No",
        views: {
            'menuContent': {
                templateUrl: "templates/menu_contact_detail_1.html",
                controller: 'Ctrl_ContactDetail1'
            }
        }
    })

    .state('menu.map', {
        url: "/map",
        views: {
            'menuContent': {
                templateUrl: "templates/menu_map.html",
                controller: 'Ctrl_Map'
            }
        }
    })

    .state('menu.map_driver', {
        url: "/map_driver",
        views: {
            'menuContent': {
                templateUrl: "templates/menu_map_driver.html",
                controller: 'Ctrl_MapDriver'
            }
        }
    })

    .state('menu.more', {
        url: "/more",
        views: {
            'menuContent': {
                templateUrl: "templates/menu_more.html",
                controller: 'Ctrl_ContactList'
            }
        }
    })

    .state('menu.message', {
        url: "/message",
        views: {
            'menuContent': {
                templateUrl: "templates/menu_message.html",
                controller: 'Ctrl_Message'
            }
        }
    })

    .state('menu.messagechat', {
        url: "/messagechat/:Id",
        views: {
            'menuContent': {
                templateUrl: "templates/menu_message_chat.html",
                controller: 'Ctrl_MessageChat'
            }
        }
    })



    .state('menu.messagegroup', {
        url: "/messagegroup",
        views: {
            'menuContent': {
                templateUrl: "templates/menu_message_group.html",
                controller: 'Ctrl_MessageGroup'
            }
        }
    })

    .state('menu.messagegroupchat', {
        url: "/messagegroupchat/:Id",
        views: {
            'menuContent': {
                templateUrl: "templates/menu_message_group_chat.html",
                controller: 'Ctrl_MessageGroupChat'
            }
        }
    })

    .state('menu.messagegroup_setting', {
        url: "/messagegroup_setting/:Id",
        views: {
            'menuContent': {
                templateUrl: "templates/menu_message_group_setting.html",
                controller: 'Ctrl_MessageGroupSetting'
            }
        }
    })

    .state('menu.messagegroup_setting_show', {
        url: "/messagegroup_setting_show/:No",
        views: {
            'menuContent': {
                templateUrl: "templates/menu_message_group_setting_show.html",
                controller: 'Ctrl_MessageGroupSetting_show'
            }
        }
    })

    .state('menu.messagegroup_setting_add', {
        url: "/messagegroup_setting_add",
        views: {
            'menuContent': {
                templateUrl: "templates/menu_message_group_setting_add.html",
                controller: 'Ctrl_MessageGroupSetting_add'
            }
        }
    })

    .state('menu.messagegroup_search', {
        url: "/messagegroup_search",
        views: {
            'menuContent': {
                templateUrl: "templates/menu_message_group_search.html",
                controller: 'Ctrl_MessageGroupSearch'
            }
        }
    })


    .state('menu.mystatus', {
        url: "/mystatus",
        views: {
            'menuContent': {
                templateUrl: "templates/menu_mystatus.html",
                controller: 'Ctrl_MyStatus'
            }
        }
    })

    .state('menu.local_joblist', {
        url: "/local_joblist",
        views: {
            'menuContent': {
                templateUrl: "templates/menu_local_job_list.html",
                controller: 'Ctrl_LocalJobList'
            }
        }
    })

    .state('menu.local_jobdetail', {
        url: "/local_jobdetail/:No",
        views: {
            'menuContent': {
                templateUrl: "templates/menu_local_job_detail.html",
                controller: 'Ctrl_LocalJobDetail'
            }
        }
    })

    .state('menu.local_schedule', {
        url: "/local_schedule",
        views: {
            'menuContent': {
                templateUrl: "templates/menu_local_schedule.html",
                controller: 'Ctrl_LocalSchedule'
            }
        }
    })

    .state('menu.local_schedule_1', {
        url: "/local_schedule_1",
        views: {
            'menuContent': {
                templateUrl: "templates/menu_local_schedule_1.html",
                controller: 'Ctrl_LocalSchedule1'
            }
        }
    })

    .state('menu.dashboard', {
        url: "/dashboard",
        views: {
            'menuContent': {
                templateUrl: "templates/menu_dashboard.html",
                controller: 'Ctrl_Dashboard'
            }
        }
    })
    .state('menu.dashboard1', {
        url: "/dashboard1/:No",
        views: {
            'menuContent': {
                templateUrl: "templates/menu_dashboard_1.html",
                controller: 'Ctrl_Dashboard1'
            }
        }
    })

    .state('menu.invoicelist', {
        url: "/invoicelist",
        views: {
            'menuContent': {
                templateUrl: "templates/menu_invoice_list.html",
                controller: 'Ctrl_InvoiceList'
            }
        }
    })

    .state('menu.invoice_detail', {
        url: "/invoice_detail/:No",
        views: {
            'menuContent': {
                templateUrl: "templates/menu_invoice_detail.html",
                controller: 'Ctrl_InvoiceDetail'
            }
        }
    })

    .state('menu.quotationlist', {
        url: "/quotationlist",
        views: {
            'menuContent': {
                templateUrl: "templates/menu_quotation_list.html",
                controller: 'Ctrl_QuotationList'
            }
        }
    })

    .state('menu.quotation_detail', {
        url: "/quotation_detail/:No",
        views: {
            'menuContent': {
                templateUrl: "templates/menu_quotation_detail.html",
                controller: 'Ctrl_QuotationDetail'
            }
        }
    })

    .state('menu.payment_list', {
        url: "/payment_list",
        views: {
            'menuContent': {
                templateUrl: "templates/menu_payment_list.html",
                controller: 'Ctrl_PaymentList'
            }
        }
    })

    .state('menu.payment_detail', {
        url: "/payment_detail/:No",
        views: {
            'menuContent': {
                templateUrl: "templates/menu_payment_detail.html",
                controller: 'Ctrl_PaymentDetail'
            }
        }
    })

    .state('menu.driver_list', {
        url: "/driver_list",
        views: {
            'menuContent': {
                templateUrl: "templates/menu_driver_list.html",
                controller: 'Ctrl_DriverList'
            }
        }
    })
    .state('menu.driver_detail', {
        url: "/driver_detail/:No",
        views: {
            'menuContent': {
                templateUrl: "templates/menu_driver_detail.html",
                controller: 'Ctrl_DriverDetail'
            }
        }
    })

    .state('menu.vehicle_list', {
        url: "/vehicle_list",
        views: {
            'menuContent': {
                templateUrl: "templates/menu_vehicle_list.html",
                controller: 'Ctrl_VehicleList'
            }
        }
    })
    .state('menu.vehicle_detail', {
        url: "/vehicle_detail/:No",
        views: {
            'menuContent': {
                templateUrl: "templates/menu_vehicle_detail.html",
                controller: 'Ctrl_VehicleDetail'
            }
        }
    })

    .state('menu.trailer_list', {
        url: "/trailer_list",
        views: {
            'menuContent': {
                templateUrl: "templates/menu_trailer_list.html",
                controller: 'Ctrl_TrailerList'
            }
        }
    })
    .state('menu.trailer_detail', {
        url: "/trailer_detail/:No",
        views: {
            'menuContent': {
                templateUrl: "templates/menu_trailer_detail.html",
                controller: 'Ctrl_TrailerDetail'
            }
        }
    })


    .state('menu.container_schedule', {
        url: "/container_schedule",
        views: {
            'menuContent': {
                templateUrl: "templates/menu_container_schedule.html",
                controller: 'Ctrl_ContainerSchedule'
            }
        }
    })

    .state('menu.stock_detail', {
        url: "/stock_detail/:No",
        views: {
            'menuContent': {
                templateUrl: "templates/menu_stock_detail.html",
                controller: 'Ctrl_StockDetail'
            }
        }
    })

    .state('menu.stock_balance_list', {
        url: "/stock_balance_list",
        views: {
            'menuContent': {
                templateUrl: "templates/menu_stock_balance_list.html",
                controller: 'Ctrl_StockBalanceList'
            }
        }
    })

    .state('menu.stock_movement_list', {
        url: "/stock_movement_list",
        views: {
            'menuContent': {
                templateUrl: "templates/menu_stock_movement_list.html",
                controller: 'Ctrl_StockMovementList'
            }
        }
    })

    .state('menu.customer_list', {
        url: "/customer_list",
        views: {
            'menuContent': {
                templateUrl: "templates/menu_customer_list.html",
                controller: 'Ctrl_CustomerList'
            }
        }
    })

    .state('menu.customer_detail', {
        url: "/customer_detail/:No",
        views: {
            'menuContent': {
                templateUrl: "templates/menu_customer_detail.html",
                controller: 'Ctrl_CustomerDetail'
            }
        }
    })

    .state('menu.daily_job_fcl', {
        url: "/daily_job_fcl",
        views: {
            'menuContent': {
                templateUrl: "templates/menu_daily_job_fcl.html",
                controller: 'Ctrl_DailyJobFCL'
            }
        }
    })

  // if none of the above states are matched, use this as the fallback
  $urlRouterProvider.otherwise('/menu/dashboard');
});

