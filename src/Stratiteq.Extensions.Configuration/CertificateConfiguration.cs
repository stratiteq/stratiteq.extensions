﻿// Copyright (c) Stratiteq Sweden AB. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;

namespace Stratiteq.Extensions.Configuration
{
    /// <summary>
    /// Contains the information needed to make authenticated requests to a web API protected with Azure Active Directory (and role based authentication) using certificates.
    /// </summary>
    public class CertificateConfiguration : AzureADConfiguration
    {
        public CertificateConfiguration()
        {
        }

        public CertificateConfiguration(IConfiguration configuration)
            : base(configuration)
        {
            this.CertificateSubjectName = configuration["CertificateSubjectName"];
            this.CertificateThumbprint = configuration["CertificateThumbprint"];
        }

        public CertificateConfiguration(string? certificateSubjectName, string? certificateThumbprint, string? appIdentifier, string? tenantId, string? clientId, string[] scopes)
            : base(appIdentifier, tenantId, clientId, scopes)
        {
            this.CertificateSubjectName = certificateSubjectName ?? throw new System.ArgumentNullException(nameof(certificateSubjectName));
            this.CertificateThumbprint = certificateThumbprint ?? throw new System.ArgumentNullException(nameof(certificateThumbprint));
        }

        public CertificateConfiguration(string certificateSubjectName, string? certificateThumbprint, AzureADConfiguration azureADConfiguration)
            : base(azureADConfiguration)
        {
            this.CertificateSubjectName = certificateSubjectName ?? throw new System.ArgumentNullException(nameof(certificateSubjectName));
            this.CertificateThumbprint = certificateThumbprint ?? throw new System.ArgumentNullException(nameof(certificateThumbprint));
        }

        public CertificateConfiguration(string certificateSubjectName, string? certificateThumbprint, string appIdentifier, AzureADConfiguration azureADConfiguration)
            : base(appIdentifier, azureADConfiguration)
        {
            this.CertificateSubjectName = certificateSubjectName ?? throw new System.ArgumentNullException(nameof(certificateSubjectName));
            this.CertificateThumbprint = certificateThumbprint ?? throw new System.ArgumentNullException(nameof(certificateThumbprint));
        }

        public CertificateConfiguration(string appIdentifier, CertificateConfiguration certificateConfiguration)
            : this(certificateConfiguration.CertificateSubjectName!, appIdentifier, certificateConfiguration)
        {
        }

        private CertificateConfiguration(CertificateConfiguration? certificateConfiguration)
            : this(
                certificateConfiguration?.CertificateSubjectName,
                certificateConfiguration?.CertificateThumbprint,
                certificateConfiguration?.AppIdentifier,
                certificateConfiguration?.TenantId,
                certificateConfiguration?.ClientId,
                certificateConfiguration?.Scopes!)
        {
        }

        /// <summary>
        /// Gets the subject name of the certificate that will be loaded and passed along the request to Azure Active Directory (AAD) to get an authentication token.
        /// The certificate (without the private key, .cer format) must be uploaded to the AAD application itself so that it can verify the certificate.
        /// The certificate (with the private key, pfx-format) must be uploaded to the web application host (App service or Azure Function).
        /// </summary>
        [Required]
        public string? CertificateSubjectName { get; internal set; }

        /// <summary>
        /// Gets the thumbprint of the certificate that will be loaded and passed along the request to Azure Active Directory (AAD) to get an authentication token.
        /// The certificate (without the private key, .cer format) must be uploaded to the AAD application itself so that it can verify the certificate.
        /// The certificate (with the private key, pfx-format) must be uploaded to the web application host (App service or Azure Function).
        /// </summary>
        [Required]
        public string? CertificateThumbprint { get; internal set; }
    }
}
