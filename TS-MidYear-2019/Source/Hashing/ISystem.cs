using System;
using System.Collections.Generic;
using System.ServiceModel;
using Acme.Clubs.Common.Data;
using Acme.Clubs.Common.Enumeration;
using Acme.Sys.Common.Data;
using Acme.Sys.Common.Enumeration;
using Acme.MemberManagement.Common.Data;
using Acme.MemberManagement.Common.Enumeration;
using System.Collections;

namespace Acme.Sys.Common.Interface
{
    [ServiceContract]
    [ServiceKnownType(typeof(Club))]
    [ServiceKnownType(typeof(TrainerSchedulingException))]
    public partial interface ISysSystem
    {

        [OperationContract]
        Customer GetCustomer(int customerID);

    }
}

