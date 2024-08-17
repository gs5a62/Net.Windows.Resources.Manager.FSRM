Windows File Server Resource Manager (FSRM)


A .Net Wrapper for FSRM (Windows File Server Resource Quota Manager) to create or update quota limit in Windows Server,
more functionality can be added like delete or list all available quotas for a path.

**Powershell modules needed to be installed :**

- `Install-WindowsFeature -Name FS-Resource-Manager -IncludeManagementTools`


Example:

`using var service = new WindowsServerQuotaManager();`

`var result = service.CreateOrUpdateQuota("d:\\targetPath", 20);`


