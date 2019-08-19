using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using Acme.ACH;
using Acme.Clubs.BusinessFacade;
using Acme.Clubs.Common.Data;
using Acme.Clubs.Common.Enumeration;
using Acme.Sys.BusinessRules;
using Acme.Sys.Common.Enumeration;
using Acme.Sys.Common.Interface;
using DAL = Acme.Sys.DataAccess;
using Acme.Sys.Properties;
using Acme.Helper;
using Acme.MemberManagement.BusinessFacade;
using Acme.MemberManagement.BusinessRules;
using Acme.MemberManagement.Common.Data;
using Acme.MemberManagement.Common.Enumeration;
using Acme.PerformanceTimer;
using GlobalPerfCounter = Acme.MemberManagement.PerformanceCounters;
using Acme.Sys.Common.Data;
using Club = Acme.Sys.Common.Data.Club;
using Customer = Acme.Sys.Common.Data.Customer;
using CustomerCommunication = Acme.Sys.Common.Data.CustomerCommunication;
using CustomerEFT = Acme.Sys.Common.Data.CustomerEFT;
using CustomerInvoice = Acme.Sys.Common.Data.CustomerInvoice;
using Employee = Acme.Sys.Common.Data.Employee;
using PhoneTypes = Acme.MemberManagement.Common.Enumeration.PhoneTypes;
using Rate = Acme.Sys.Common.Data.Rate;
using BillingHistoryData = Acme.Sys.Common.Data.BillingHistory;
using PromotionalRateData = Acme.Sys.Common.Data.PromotionalRate;
using Acme.Sys.Common;
using Acme.HumanResources.BusinessFacade;
using Acme.HumanResources.Common.Enumeration;
using Acme.Sys.DocumentService;
using System.Threading.Tasks;
using System.Transactions;
using Acme.GuestPass.Common;
using Acme.GuestPass;
using Acme.GuestPass.Common.Enumeration;
using Acme.GuestPass.BusinessFacade;
using System.Linq;
using CoreAPI_GiftCard = Acme.API.Common.Model.Product.GiftCard;
using CoreAPI_ProductEnum = Acme.API.Common.Model.Product.Enum;
using CoreAPI_Enum = Acme.API.Common.Model.Enum;
using CoreAPI_General = Acme.API.Common.Model.General;
using CoreAPI_Email = Acme.API.Common.Model.Communication.Email;
using Acme.API.Common.Model.Product.Enum;

namespace Acme.Sys.BusinessFacade
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
      ConcurrencyMode = ConcurrencyMode.Multiple, AddressFilterMode = AddressFilterMode.Any)]
    [GlobalErrorHandlerBehavior(typeof(GlobalErrorHandler))]
    public partial class SysSystem : MarshalByRefObject, ISysSystem
    {
        /// <summary>
        /// This implements the interface.
        /// </summary>
        public Customer GetCustomer(int customerID)
        {
            SqlConnection connection = null;
            CustomerCollection cc = null;
            QueryPerfCounter timer = new QueryPerfCounter();
            timer.Start();
            try
            {
                connection = Connection.GetAcmeConnection();
                cc = DataAccess.Customer.Customer_SelectByID_Customer_Status_Services(customerID, connection, null);
            }
            catch (Exception ex)
            {
		LogHelper.LogError("Acme.Sys.Exception", ex, false, MethodInfo.GetCurrentMethod(), customerID);
                throw;
            }
            finally
            {
                if (connection != null)
                    connection.Close();
                timer.Stop();
                perfMon.GetCustomer.Process(timer.TotalTicks);
            }
            if (cc.Count > 0)
                return cc[0];
            else
                return null;
        }

        /// <summary>
        /// This does NOT implements the interface.
        /// </summary>
        public Customer GetCustomer(int customerID, SqlConnection connection, SqlTransaction transaction)
        {
            CustomerCollection cc = null;
            QueryPerfCounter timer = new QueryPerfCounter();
            timer.Start();
            try
            {
                cc = DataAccess.Customer.Customer_SelectByID_Customer_Status_Services(customerID, connection, transaction);
            }
            catch (Exception ex)
            {
                LogHelper.LogError("Acme.Sys.Exception", ex, false, MethodInfo.GetCurrentMethod(), customerID);
                throw;
            }
            finally
            {
                timer.Stop();
                perfMon.GetCustomer.Process(timer.TotalTicks);
            }
            if (cc.Count > 0)
                return cc[0];
            else
                return null;
        }
    }
}
