# WindowsResourcesManager FSRM

WindowsResourcesManager (FSRM) is a wrapper designed to simplify the creation and management of folder quotas on Windows
Server.
It addresses the common challenge of managing disk space usage by providing developers with a straightforward API to
configure folder quotas programmatically.

## Features

- Create hard quota limit.
- Update hard quota limit.
- Delete quota.
- More will be implemented later.

### Requirements

- **Operating System**: Windows Server 2012 or later with windows feature FS-Resource-Manager installed.
- **.NET Framework**: .NET Framework 4.6.1 or later, .NET Core 3.1 or later or netstandard 2.0 or later.
- **Permissions**: Elevated permissions.

## Getting Started

### Installation

You can install the package via NuGet Package Manager:

```bash
Install-Package WindowsResourcesManager.FSRM
```

Install the windows feature if it's not already installed

```bash
Install-WindowsFeature -Name FS-Resource-Manager -IncludeManagementTools
```

### How to Use

Example setting a 20 GB hard limit on folder `d:\\targetPath`

```bash
using var service = new WindowsServerQuotaManager();

var result = service.CreateOrUpdateQuota("d:\\targetPath", 20);
```



