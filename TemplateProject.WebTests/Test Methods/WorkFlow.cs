using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace TemplateProject.WebTests.Test_Methods
{
    public partial class TestMethods : BaseWebTest
    {
        [Given("Username (.*) logged in with password (.*)")]
        public void GivenUserLoggedIn(string username, string password)
        {
            GivenIAmOnPage("Account/Login");
            WhenTextEntered(username, "UserName");
            WhenTextEntered(password, "Password");
            WhenElementIdClicked("submit");
            ThenThePageShouldBeDisplayed("");
        }

        [When("user navigates to Order Management")]
        public void WhenUserNavigatesToOrderManagement()
        {
            WhenPageReset();
            WhenElementIdClicked("menuOrders");
            WhenElementIdClicked("menuOrderManagement");
            ThenThePageShouldBeDisplayed("Orders");
        }

        [When("user navigates to pumps")]
        public void WhenUserNavigatesToPumps()
        {
            WhenPageReset();
            WhenElementIdClicked("menuInventory");
            WhenElementIdClicked("menuPumps");
            ThenThePageShouldBeDisplayed("Inventory/Pumps");
        }

        [When("user navigates to supplies")]
        public void WhenUserNavigatesToSupplies()
        {
            WhenPageReset();
            WhenElementIdClicked("menuInventory");
            WhenElementIdClicked("menuSupplies");
            ThenThePageShouldBeDisplayed("Inventory/Supplies");
        }

        [When(@"user navigates to Email Templates")]
        public void WhenUserNavigatesToEmailTemplates()
        {
            WhenPageReset();
            WhenElementIdClicked("menuEmailTemplate");
            ThenThePageShouldBeDisplayed("EmailTemplate/EmailTemplate");
        }

        [When(@"user navigates to add cooler order page")]
        private void WhenUserNavigatesToAddCoolerOrderPage()
        {
            WhenPageReset();
            WhenElementIdClicked("menuOrders");
            WhenElementIdClicked("menuCreateCoolerOrder");
            ThenThePageShouldBeDisplayed("Order/AddCoolerOrder");
        }

        [When(@"user navigates to add pump supply order page")]
        private void WhenUserNavigatesToAddPumpSupplyOrderPage()
        {
            WhenPageReset();
            WhenElementIdClicked("menuOrders");
            WhenElementIdClicked("menuCreatePump/SupplyOrder");
            ThenThePageShouldBeDisplayed("Order/AddPumpSupplyOrder");
        }

        [When(@"cooler order is created")]
        public void WhenCoolerOrderCeated()
        {
            WhenPageReset();
            WhenUserNavigatesToAddCoolerOrderPage();
            WhenTextEntered("60002", "Order_DonorID");
            WhenElementIdClicked("updateDonorInfo");
            WhenValueEnteredInNumericWithId("1", "DoublePumpingKit");
            WhenValueEnteredInNumericWithId("1", "24mmBreastshields");
            WhenValueEnteredInNumericWithId("1", "27mmBreastshields");
            WhenValueEnteredInNumericWithId("1", "30mmBreastshields");
            WhenValueEnteredInNumericWithId("1", "8ozBottlesSet");
            WhenValueEnteredInNumericWithId("1", "PersonalFitConnectors");
            WhenValueEnteredInNumericWithId("1", "ValvesandMembranes");
            WhenValueEnteredInNumericWithId("1", "Membranes");
            WhenValueEnteredInNumericWithId("1", "Tubing");
            WhenValueEnteredInNumericWithId("1", "Bags");
            WhenValueEnteredInNumericWithId("1", "YellowBag");
            WhenValueEnteredInNumericWithId("1", "DoublePumpingKit");
            WhenValueEnteredInNumericWithId("1", "DoublePumpingKit");
            WhenTextEntered("Test Note", "Order_Notes");
            WhenElementIdClicked("btnPlaceOrder");
            ThenThePageShouldBeDisplayed("Order/OrderDetail");
        }

        [When(@"cooler packet is printed")]
        public void WhenCoolerPacketIsPrinted()
        {
            WhenUserNavigatesToOrderManagement();
            WhenRowContainingTextInGridClicked("Not Printed", "coolerOrdersGrid");
            WhenElementIdClicked("printPackets");
            WhenSwitchToNewTab();
            ThenThePageShouldBeDisplayed("Order/CoolerPacket");
            WhenSwitchToOldTab();
        }

        [When(@"cooler order fulfilled (.*)")]
        public void WhenCoolerOrderFulfilled(string donorId)
        {
            WhenFilterColumnInGrid("Pending", "Status", "coolerOrdersGrid");
            WhenElementWithClassClickedInGridWithRowContainingText("k-grid-Fulfill", "coolerOrdersGrid", donorId);
            ThenThePageShouldBeDisplayed("Order/Step1");
            WhenRadioButtonWithIdClicked("ShippingMethod_Ground");
            WhenTextEntered("1223-3344-4455", "TrackingNumber");
            WhenElementIdClicked("btnSubmitFulfillment");
            ThenThePageShouldBeDisplayed("Order/Step2");
            WhenTextEntered("9988-7766-5544", "ReturnTrackingNumber");
            WhenElementIdClicked("btnSubmitFulfillment");
            ThenThePageShouldBeDisplayed("Order/Step3");
            WhenElementIdClicked("btnOrderManagement");
            ThenThePageShouldBeDisplayed("Orders");
        }

        [When(@"cooler order verified (.*)")]
        public void WhenCoolerOrderVerified(string donorId)
        {
            WhenUserLogsOut();
            GivenUserLoggedIn("MbtsWHTester1", "Prolacta99");
            WhenUserNavigatesToOrderManagement();
            WhenFilterColumnInGrid("Fulfilled", "Status", "coolerOrdersGrid");
            WhenElementWithClassClickedInGridWithRowContainingText("k-grid-Verify", "coolerOrdersGrid", donorId);
            ThenThePageShouldBeDisplayed("Order/Verify");
            WhenElementIdClicked("btnSubmitVerification");
            ThenThePageShouldBeDisplayed("Order/Verified");
            WhenElementIdClicked("btnOrderManagement");
            ThenThePageShouldBeDisplayed("Orders");
        }

        [When(@"pump order created")]
        public void WhenPumpOrderCreated()
        {
            WhenAddNewPump();
            WhenUserNavigatesToAddPumpSupplyOrderPage();
            WhenTextEntered("60002", "Order_DonorID");
            WhenElementIdClicked("updateDonorInfo");
            WhenElementIdClicked("showPump");
            WhenTextEntered("987654321", "PumpSerial");
            WhenValueEnteredInNumericWithId("1", "DoublePumpingKit");
            WhenValueEnteredInNumericWithId("1", "24mmBreastshields");
            WhenValueEnteredInNumericWithId("1", "27mmBreastshields");
            WhenValueEnteredInNumericWithId("1", "30mmBreastshields");
            WhenValueEnteredInNumericWithId("1", "8ozBottlesSet");
            WhenValueEnteredInNumericWithId("1", "PersonalFitConnectors");
            WhenValueEnteredInNumericWithId("1", "ValvesandMembranes");
            WhenValueEnteredInNumericWithId("1", "Membranes");
            WhenValueEnteredInNumericWithId("1", "Tubing");
            WhenValueEnteredInNumericWithId("1", "Bags");
            WhenValueEnteredInNumericWithId("1", "YellowBag");
            WhenValueEnteredInNumericWithId("1", "DoublePumpingKit");
            WhenValueEnteredInNumericWithId("1", "DoublePumpingKit");
            WhenTextEntered("Test Note", "Order_Notes");
            WhenElementIdClicked("btnPlaceOrder");
            ThenThePageShouldBeDisplayed("Order/OrderDetail");
        }

        [When(@"bag only order created")]
        public void WhenBagOnlyOrderCreated()
        {
            WhenUserNavigatesToAddPumpSupplyOrderPage();
            WhenTextEntered("60002", "Order_DonorID");
            WhenElementIdClicked("updateDonorInfo");
            WhenValueEnteredInNumericWithId("5", "Bags");
            WhenTextEntered("Test Note", "Order_Notes");
            WhenElementIdClicked("btnPlaceOrder");
            ThenThePageShouldBeDisplayed("Order/OrderDetail");
        }

        [When(@"pump order fulfilled (.*)")]
        public void WhenPumpOrderFulfilled(string donorId)
        {
            WhenElementIdClicked("pumpSupplyTab");
            ThenElementHasClass("pumpSupplyTab", "k-state-active");
            WhenFilterColumnInGrid("Pending", "Status", "pumpOrdersGrid");
            WhenElementWithClassClickedInGridWithRowContainingText("k-grid-Fulfill", "pumpOrdersGrid", donorId);
            ThenThePageShouldBeDisplayed("Order/Step1");
            WhenRadioButtonWithIdClicked("ShippingMethod_Ground");
            WhenTextEntered("1223-3344-4455", "TrackingNumber");
            WhenElementIdClicked("btnSubmitFulfillment");
            ThenThePageShouldBeDisplayed("Order/Step2");
            WhenTextEntered("9988-7766-5544", "ReturnTrackingNumber");
            WhenElementIdClicked("btnSubmitFulfillment");
            ThenThePageShouldBeDisplayed("Order/Step3");
            WhenElementIdClicked("btnOrderManagement");
            ThenThePageShouldBeDisplayed("Orders");
        }

        [When(@"new pump added")]
        public void WhenAddNewPump()
        {
            WhenUserNavigatesToPumps();
            WhenElementIdClicked("btnAddPump");
            WhenTextEntered("987654321", "PumpSerialNumber");
            WhenTextEntered("01/11/2018", "DateAcquired");
            WhenDropDownPicked("pumpStatus", "Available");
            WhenTextEntered("0", "HoursUsed");
            WhenTextEntered("Test Note", "Notes");
            WhenElementClassClicked("k-grid-update");
        }

        [When(@"thank you email template created")]
        public void WhenThankYouEmailTemplateCreated()
        {
            WhenUserNavigatesToEmailTemplates();
            WhenElementIdClicked("btnAddEmailTemplate");
            WhenDropDownPicked("TemplateType", "Thank You Letter");
            WhenDropDownPicked("TemplateMilkBank", "Tiny Treasures Milk Bank");
            WhenTextEntered("Test TTB Thank You", "TemplateTitle");
            WhenTextEntered("TTTY", "ShortTitle");
            WhenTextEntered("Test Subject", "TemplateSubject");
            WhenTextEntered("Test Body", "TemplateBody");
            WhenTextEntered("Test Signature", "TemplateSignature");
            WhenElementClassClicked("k-grid-update");
        }

        [When(@"email template deleted (.*)")]
        public void WhenEmailTemplateDeleted(string emailTemplateName)
        {
            WhenUserNavigatesToEmailTemplates();
            WhenElementWithClassClickedInGridWithRowContainingText("k-grid-delete", "EmailTemplate", emailTemplateName);
            WhenClickAlertBoxOk(true);
        }

        [When(@"follow up email template created")]
        public void WhenFollowUpEmailTemplateCreated()
        {
            WhenUserNavigatesToEmailTemplates();
            WhenElementIdClicked("btnAddEmailTemplate");
            WhenDropDownPicked("TemplateType", "HH Follow Up");
            WhenDropDownPicked("TemplateMilkBank", "Helping Hands (Komen) Milk Bank");
            WhenTextEntered("Test HH Follow Up", "TemplateTitle");
            WhenTextEntered("THHFU", "ShortTitle");
            WhenTextEntered("Test Subject", "TemplateSubject");
            WhenTextEntered("Test Body", "TemplateBody");
            WhenTextEntered("Test Signature", "TemplateSignature");
            WhenElementClassClicked("k-grid-update");
        }

        [When(@"user logs out")]
        public void WhenUserLogsOut()
        {
            WhenElementIdClicked("userDropdown");
            WhenElementIdClicked("loginLink");
            ThenThePageShouldBeDisplayed("Account/Login");
        }

    }
}
