// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
// 
// Code generated by Microsoft (R) AutoRest Code Generator 0.17.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Microsoft.Azure.Management.Graph.RBAC.Models
{
    using System.Linq;

    /// <summary>
    /// Request parameters for create a new service principal
    /// </summary>
    public partial class ServicePrincipalCreateParametersInner
    {
        /// <summary>
        /// Initializes a new instance of the
        /// ServicePrincipalCreateParametersInner class.
        /// </summary>
        public ServicePrincipalCreateParametersInner() { }

        /// <summary>
        /// Initializes a new instance of the
        /// ServicePrincipalCreateParametersInner class.
        /// </summary>
        /// <param name="appId">application Id</param>
        /// <param name="accountEnabled">Specifies if the account is
        /// enabled</param>
        /// <param name="keyCredentials">the list of KeyCredential
        /// objects</param>
        /// <param name="passwordCredentials">the list of PasswordCredential
        /// objects</param>
        public ServicePrincipalCreateParametersInner(string appId, bool accountEnabled, System.Collections.Generic.IList<KeyCredential> keyCredentials = default(System.Collections.Generic.IList<KeyCredential>), System.Collections.Generic.IList<PasswordCredential> passwordCredentials = default(System.Collections.Generic.IList<PasswordCredential>))
        {
            AppId = appId;
            AccountEnabled = accountEnabled;
            KeyCredentials = keyCredentials;
            PasswordCredentials = passwordCredentials;
        }

        /// <summary>
        /// Gets or sets application Id
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "appId")]
        public string AppId { get; set; }

        /// <summary>
        /// Gets or sets specifies if the account is enabled
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "accountEnabled")]
        public bool AccountEnabled { get; set; }

        /// <summary>
        /// Gets or sets the list of KeyCredential objects
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "keyCredentials")]
        public System.Collections.Generic.IList<KeyCredential> KeyCredentials { get; set; }

        /// <summary>
        /// Gets or sets the list of PasswordCredential objects
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "passwordCredentials")]
        public System.Collections.Generic.IList<PasswordCredential> PasswordCredentials { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="Microsoft.Rest.ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (AppId == null)
            {
                throw new Microsoft.Rest.ValidationException(Microsoft.Rest.ValidationRules.CannotBeNull, "AppId");
            }
        }
    }
}