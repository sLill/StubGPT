﻿namespace StubGPT.Common;
public interface IServiceModule
{
    #region Methods..
    IServiceCollection AddServices(IServiceCollection services);
    #endregion Methods..
}

public interface IServiceModule<T>
{
    #region Methods..
    IServiceCollection AddServices(IServiceCollection services, [Optional] T configuration);
    #endregion Methods..
}
