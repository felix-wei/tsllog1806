﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateTransport2_Detail.aspx.cs" Inherits="PagesContTrucking_Daily_UpdateTransport2_Detail" %>

<div>
    <div class="tabs" ng-init="common_tabs_current_tab='tab0'">
        <div class="tab">
            <div ng-click="common_tabs_current_tab='tab0';" ng-class="{true:'tab_select'}[common_tabs_current_tab=='tab0']">Detail</div>
            <div ng-click="common_tabs_current_tab='tab1';" ng-class="{true:'tab_select'}[common_tabs_current_tab=='tab1']">&nbsp;&nbsp;Fee&nbsp;&nbsp;</div>
            <div ng-click="common_tabs_current_tab='tab10';" ng-class="{true:'tab_select'}[common_tabs_current_tab=='tab10']">More</div>
            <div ng-click="common_tabs_current_tab='tab11';" ng-class="{true:'tab_select'}[common_tabs_current_tab=='tab11']" ng-show="vm&&vm.emailTab">Email</div>

        </div>
        <div class="tabs_content">
            <!--tab0-->
            <div ng-show="common_tabs_current_tab=='tab0'">

                <table class="bx_table_100pc bx_table_grid bx_table_grid_border0">
                    <tr class="tr_label">
                        <td>Driver:</td>
                        <td>Attendant:</td>
                    </tr>
                    <tr>
                        <td>

                            <div class="single_buttonselect_full">
                                <div class="code0 code0_full">
                                    <input type="text" ng-model="vm.curView.mast.DriverCode" ng-change="floatView.changeView();" />
                                    <a class="bx_a_button" ng-click="floatView.selectDriver();">&nbsp;.. </a>
                                </div>
                            </div>
                        </td>
                        <td>

                            <div class="single_buttonselect_full">
                                <div class="code0 code0_full">
                                    <input type="text" ng-model="vm.curView.mast.DriverCode2" ng-change="floatView.changeView();" />
                                    <a class="bx_a_button" ng-click="floatView.selectDriver2();">&nbsp;.. </a>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr class="tr_label">
                        <td>Driver2:</td>
                        <td>Attendant2:</td>
                    </tr>
                    <tr>
                        <td>

                            <div class="single_buttonselect_full">
                                <div class="code0 code0_full">
                                    <input type="text" ng-model="vm.curView.mast.DriverCode11" ng-change="floatView.changeView();" />
                                    <a class="bx_a_button" ng-click="floatView.selectDriver11();">&nbsp;.. </a>
                                </div>
                            </div>
                        </td>
                        <td>

                            <div class="single_buttonselect_full">
                                <div class="code0 code0_full">
                                    <input type="text" ng-model="vm.curView.mast.DriverCode12" ng-change="floatView.changeView();" />
                                    <a class="bx_a_button" ng-click="floatView.selectDriver12();">&nbsp;.. </a>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr class="tr_label">
                        <td>Trip Status:</td>
                    </tr>
                    <tr>
                        <td>
                            <select ng-model="vm.curView.mast.Statuscode" class="single_select_full" ng-change="floatView.changeView();">
                                <option value="P">Pending</option>
                                <option value="A">Accepted</option>
                                <option value="S">Started</option>
                                <option value="C">Delivered</option>
                                <option value="X">Cancel</option>
                            </select>
                        </td>
                    </tr>
                    <tr class="tr_label">
                        <td>Trip Instruction:</td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <textarea ng-model="vm.curView.mast.Remark" class="single_textarea_full" ng-change="floatView.changeView();"></textarea>
                        </td>
                    </tr>
                    <tr class="tr_label">
                        <td>Prime Mover:</td>
                        <td>Trailer:</td>
                    </tr>
                    <tr>
                        <td>
                            <div class="single_buttonselect_full">
                                <div class="code0 code0_full">
                                    <input type="text" ng-model="vm.curView.mast.TowheadCode" ng-change="floatView.changeView();" />
                                    <a class="bx_a_button" ng-click="floatView.selectTowhead();">&nbsp;.. </a>
                                </div>
                            </div>
                        </td>
                        <!--<td>
                            <div class="single_buttonselect_full">
                                <div class="code0 code0_full">
                                    <input type="text" ng-model="vm.curView.mast.ChessisCode" ng-change="floatView.changeView();" />
                                    <a class="bx_a_button" ng-click="floatView.selectTrailer();">&nbsp;.. </a>
                                </div>
                            </div>
                        </td>-->
                        <td>
                            <input type="text" class="single_text_full" ng-model="vm.curView.mast.ContainerNo" ng-change="floatView.changeView();" /></td>
                    </tr>
                    <tr class="tr_label">
                        <td>SubContract:</td>
                        <td>SubContractCode:</td>
                    </tr>
                    <tr>
                        <td>
                            <select ng-model="vm.curView.mast.SubCon_Ind" class="single_select_full" ng-change="floatView.changeView();">
                                <option value="Y">YES</option>
                                <option value="N">NO</option>
                            </select>
                        </td>
                        <td>
                            <div class="single_buttonselect_full">
                                <div class="code0 code0_full">
                                    <input type="text" ng-model="vm.curView.mast.SubCon_Code" ng-change="floatView.changeView();" />
                                    <a class="bx_a_button" ng-click="floatView.selectSubContractCode();">&nbsp;.. </a>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr class="tr_label">
                        <td>FromDate:</td>
                        <td>Time:</td>
                    </tr>
                    <tr>
                        <!--<td><input type="text" class="single_text_full" ng-model="vm.curView.mast.FromDate" placeholder="yyyy/MM/dd" ng-change="floatView.changeView();" /></td>-->
                        <td>
                            <input type="date" class="single_text_full" ng-model="vm.curView.mast.FromDate" ng-change="floatView.changeView();" /></td>
                        <td>
                            <input type="text" class="single_text_full" ng-model="vm.curView.mast.FromTime" placeholder="HH:mm" ng-change="floatView.changeView();" /></td>
                    </tr>
                    <!--<tr class="tr_label">
                        <td colspan="2">Trailer From ParkingLot:</td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div class="single_buttonselect_full">
                                <div class="code0 code0_full">
                                    <input type="text" ng-model="vm.curView.mast.FromParkingLot" ng-change="floatView.changeView();" />
                                    <a class="bx_a_button" ng-click="floatView.selectFromParkingLot();">&nbsp;.. </a>
                                </div>
                            </div>
                        </td>
                    </tr>-->
                    <tr class="tr_label">
                        <td>Address:</td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <textarea ng-model="vm.curView.mast.FromCode" class="single_textarea_full" ng-change="floatView.changeView();"></textarea>
                        </td>
                    </tr>
                    <tr class="tr_label">
                        <td>ToDate:</td>
                        <td>Time:</td>
                    </tr>
                    <tr>
                        <td>
                            <input type="date" class="single_text_full" ng-model="vm.curView.mast.ToDate" ng-change="floatView.changeView();" /></td>
                        <td>
                            <input type="text" class="single_text_full" ng-model="vm.curView.mast.ToTime" placeholder="HH:mm" ng-change="floatView.changeView();" /></td>
                    </tr>
                    <!--<tr class="tr_label">
                        <td>Trailer To ParkingLot:</td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div class="single_buttonselect_full">
                                <div class="code0 code0_full">
                                    <input type="text" ng-model="vm.curView.mast.ToParkingLot" ng-change="floatView.changeView();" />
                                    <a class="bx_a_button" ng-click="floatView.selectToParkingLot();">&nbsp;.. </a>
                                </div>
                            </div>
                        </td>
                    </tr>-->
                    <tr class="tr_label">
                        <td>Address:</td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <textarea ng-model="vm.curView.mast.ToCode" class="single_textarea_full" ng-change="floatView.changeView();"></textarea>
                        </td>
                    </tr>
                    <tr class="tr_label">
                        <td>Driver Remark:</td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <textarea ng-model="vm.curView.mast.Remark1" class="single_textarea_full" ng-change="floatView.changeView();"></textarea>
                        </td>
                    </tr>

                </table>
            </div>
            <!--tab1-->
            <div ng-show="common_tabs_current_tab=='tab1'">

                <table class="bx_table_100pc bx_table_grid bx_table_grid_border0">
                    <tr class="tr_label">
                        <td>Driver Trip($)</td>
                        <td>Overtime($)</td>
                    </tr>
                    <tr>
                        <td>
                            <input type="number" class="single_text_full" ng-model="vm.curView.mast.d_inc1" ng-change="floatView.changeView();" /></td>
                        <td>
                            <input type="number" class="single_text_full" ng-model="vm.curView.mast.d_inc2" ng-change="floatView.changeView();" /></td>
                    </tr>
                    <tr class="tr_label">
                        <td>Standby($)</td>
                    </tr>
                    <tr>
                        <td>
                            <input type="number" class="single_text_full" ng-model="vm.curView.mast.d_inc3" ng-change="floatView.changeView();" /></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="border-bottom: 1px solid red"></td>
                    </tr>
                    <tr class="tr_label">
                        <td>Attendant Trip($)</td>
                        <td>Overtime($)</td>
                    </tr>
                    <tr>
                        <td>
                            <input type="number" class="single_text_full" ng-model="vm.curView.mast.a_inc1" ng-change="floatView.changeView();" /></td>
                        <td>
                            <input type="number" class="single_text_full" ng-model="vm.curView.mast.a_inc2" ng-change="floatView.changeView();" /></td>
                    </tr>
                    <tr class="tr_label">
                        <td>Standby($)</td>
                    </tr>
                    <tr>
                        <td>
                            <input type="number" class="single_text_full" ng-model="vm.curView.mast.a_inc3" ng-change="floatView.changeView();" /></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="border-bottom: 1px solid red"></td>
                    </tr>
                    <tr class="tr_label">
                        <td>Driver2 Trip($)</td>
                        <td>Overtime($)</td>
                    </tr>
                    <tr>
                        <td>
                            <input type="number" class="single_text_full" ng-model="vm.curView.mast.d2_inc1" ng-change="floatView.changeView();" /></td>
                        <td>
                            <input type="number" class="single_text_full" ng-model="vm.curView.mast.d2_inc2" ng-change="floatView.changeView();" /></td>
                    </tr>
                    <tr class="tr_label">
                        <td>Standby($)</td>
                    </tr>
                    <tr>
                        <td>
                            <input type="number" class="single_text_full" ng-model="vm.curView.mast.d2_inc3" ng-change="floatView.changeView();" /></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="border-bottom: 1px solid red"></td>
                    </tr>
                    <tr class="tr_label">
                        <td>Attendant2 Trip($)</td>
                        <td>Overtime($)</td>
                    </tr>
                    <tr>
                        <td>
                            <input type="number" class="single_text_full" ng-model="vm.curView.mast.a2_inc1" ng-change="floatView.changeView();" /></td>
                        <td>
                            <input type="number" class="single_text_full" ng-model="vm.curView.mast.a2_inc2" ng-change="floatView.changeView();" /></td>
                    </tr>
                    <tr class="tr_label">
                        <td>Standby($)</td>
                    </tr>
                    <tr>
                        <td>
                            <input type="number" class="single_text_full" ng-model="vm.curView.mast.a2_inc3" ng-change="floatView.changeView();" /></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="border-bottom: 1px solid red"></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="border-bottom: 1px solid red"></td>
                    </tr>
                    <tr class="tr_label">
                        <td>Billing($)</td>
                        <td>Overtime($)</td>
                    </tr>
                    <tr>
                        <td>
                            <input type="number" class="single_text_full" ng-model="vm.curView.mast.inc1" ng-change="floatView.changeView();" /></td>
                        <td>
                            <input type="number" class="single_text_full" ng-model="vm.curView.mast.inc2" ng-change="floatView.changeView();" /></td>
                    </tr>
                    <tr class="tr_label">
                        <td>Permit($)</td>
                    </tr>
                    <tr>
                        <td>
                            <input type="number" class="single_text_full" ng-model="vm.curView.mast.inc3" ng-change="floatView.changeView();" /></td>
                    </tr>
                    <tr class="tr_label">
                        <td>Billing Remark</td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <textarea ng-model="vm.curView.mast.BillingRemark" class="single_textarea_full" ng-change="floatView.changeView();"></textarea>
                        </td>
                    </tr>
                </table>
            </div>
            <!--tab2-->
            <!--<div ng-show="common_tabs_current_tab=='tab2'">

                <div class="bx_table bx_table_100pc bx_table_nofocus_row">
                    <div class="header">
                        <div>Attachment</div>
                        <div></div>
                    </div>
                    <div class="body">
                        <div class="item" ng-repeat="row in vm.curView.attachment">
                            <div>
                                <a href="{{row.FilePath}}" target="_blank">
                                    <img src="{{row.FilePath}}" style="width:100px;height:100px" />
                                </a>
                            </div>
                            <div>{{row.FileName}}</div>
                        </div>
                    </div>
                </div>
            </div>-->
            <!--tab10-->
            <div ng-show="common_tabs_current_tab=='tab10'">
                <table class="bx_table_100pc bx_table_grid bx_table_grid_border0">
                    <!--<tr>
                        <td>
                            <input type="button" class="button" value="Add Empty SHF" ng-show="vm.curView.mast.JobStatusCode=='USE'" ng-click="floatView.addNewTrip('SHF');" /></td>
                    </tr>
                    <tr>
                        <td><input type="button" class="button" value="Add Empty LOC" ng-show="vm.curView.mast.JobStatusCode=='USE'" ng-click="floatView.addNewTrip('LOC');" /></td>
                    </tr>-->
                    
                    <tr>
                        <td><input type="button" class="button" value="Show Attachments" ng-click="attachment.showAttachments(vm.curView.mast.JobNo);" /></td>
                    </tr>

                    <tr>
                        <td><input type="button" class="button" value="Ready for Billing" ng-show="vm.curView.mast.JobStatus!='Billing'" ng-click="floatView.readyForBilling();" /></td>
                    </tr>

					</table>
            </div>
            <!--tab11-->
            <div ng-show="common_tabs_current_tab=='tab11'">
                <form name="fEmail" ng-submit="floatView.sendEmail();" ng-if="vm&&vm.emailTab">
                    <table class="bx_table_100pc bx_table_grid bx_table_grid_border0">
                        <tr class="tr_label">
                            <td>Email To <b style="color: red;">(*)</b> :</td>
                        </tr>
                        <tr>
                            <td>
                                <input type="email" ng-model="vm.emailTab.emailTo" class="single_text_full" placeholder="To@email.address" required /></td>
                        </tr>
                        <tr class="tr_label">
                            <td>Cc:</td>
                        </tr>
                        <tr>
                            <td>
                                <input type="email" ng-model="vm.emailTab.emailCc" class="single_text_full" placeholder="Cc@email.address" /></td>
                        </tr>
                        <tr class="tr_label">
                            <td>Subject <b style="color: red;">(*)</b> :</td>
                        </tr>
                        <tr>
                            <td>
                                <input type="text" ng-model="vm.emailTab.emailSubject" class="single_text_full" required /></td>
                        </tr>
                        <tr>
                            <td>
                                <textarea ng-model="vm.emailTab.emailContent" class="single_textarea_full" style="height: 200px;"></textarea>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div ng-hide="vm.emailTab.sending">
                                    <input type="submit" class="button" value="Send" ng-disabled="fEmail.$invalid" />
                                </div>
                                <div ng-show="vm.emailTab.sending">
                                    <input type="button" class="button" value="Sending..." ng-disabled="true" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </form>
            </div>
        </div>


    </div>

</div>