using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace DataCenterLive.Clients
{
    public static class ServicesHelper
    {
        /// <summary>
        /// The base namespace used for webservice services and types.  Note this needs to be constant to work in attributes.
        /// </summary>
        private const string BaseNameSpace = "http://schemas.mt.com/AutoChem/CentralDataServer";

        /// <summary>
        /// The namespace used for webservice services.  Note this needs to be constant to work in attributes.
        /// </summary>
        public const string ServiceNameSpace = BaseNameSpace + "/Services";

        /// <summary>
        /// The namespace used for webservice types.  Note this needs to be constant to work in attributes.
        /// </summary>
        public const string TypeNameSpace = BaseNameSpace + "/Types";

        /// <summary>
        /// Gets the default binding for WPF WCF communications.
        /// </summary>        
        public static Binding GetDefaultBinding()
        {
            BasicHttpBinding binding = new BasicHttpBinding(BasicHttpSecurityMode.TransportCredentialOnly);
            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Windows;
            binding.MaxReceivedMessageSize = 16000000;
            binding.ReaderQuotas.MaxArrayLength = 16000000;
            binding.ReceiveTimeout = TimeSpan.FromMinutes(15);
            binding.SendTimeout = TimeSpan.FromMinutes(15);

            return binding;
        }

    }
}
