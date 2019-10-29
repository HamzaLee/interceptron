# Interceptron

Interceptron is a set of packages that add interception to the default dependency injection container.

## License

![GitHub](https://img.shields.io/github/license/HamzaLee/interceptron)

## Usage

The package extends the default dependency injection container by adding overloads of the existant methods that accept interceptors as parameters.

Example:
```
services.AddTransient<ICustomService, CustomService>(new IInterceptor[] { new DebuggerInterceptor() });
```
## Build

| Platforms       | Master       | Develop    |
|-----------------|-------------:|------------|
| Windows|[![Build Status](https://dev.azure.com/HamzaLee/Interceptron/_apis/build/status/HamzaLee.interceptron?branchName=master)](https://dev.azure.com/HamzaLee/Interceptron/_build/latest?definitionId=1&branchName=master) |[![Build Status](https://dev.azure.com/HamzaLee/interceptron/_apis/build/status/HamzaLee.interceptron?branchName=develop)](https://dev.azure.com/HamzaLee/interceptron/_build/latest?definitionId=1&branchName=develop)

## Tests

![Azure DevOps coverage](https://img.shields.io/azure-devops/coverage/HamzaLee/interceptron/1)

![Azure DevOps tests](https://img.shields.io/azure-devops/tests/HamzaLee/interceptron/1)

## Issues

[![GitHub issues](https://img.shields.io/github/issues/HamzaLee/interceptron)](https://github.com/HamzaLee/interceptron/issues)

[![GitHub pull requests](https://img.shields.io/github/issues-pr/HamzaLee/interceptron)](https://github.com/HamzaLee/interceptron/pulls)

## Release

### Core
[![NuGet](https://img.shields.io/nuget/v/Interceptron.Core)](https://www.nuget.org/packages/Interceptron.Core/)
[![NuGet](https://img.shields.io/nuget/dt/Interceptron.Core)](https://www.nuget.org/packages/Interceptron.Core/)

### DynamicProxy
[![NuGet](https://img.shields.io/nuget/v/Interceptron.DynamicProxy)](https://www.nuget.org/packages/Interceptron.DynamicProxy/)
[![NuGet](https://img.shields.io/nuget/dt/Interceptron.DynamicProxy)](https://www.nuget.org/packages/Interceptron.DynamicProxy/)

### DispatchProxy
[![NuGet](https://img.shields.io/nuget/v/Interceptron.DispatchProxy)](https://www.nuget.org/packages/Interceptron.DispatchProxy/)
[![NuGet](https://img.shields.io/nuget/dt/Interceptron.DispatchProxy)](https://www.nuget.org/packages/Interceptron.DispatchProxy/)

