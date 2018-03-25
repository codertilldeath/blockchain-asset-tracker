﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainAPI
{
    public interface IBlockchainService
    {
        Task<String> InvokeGet(String url);
        Task<String> InvokePost(String url, String jsonObject);
    }
}
