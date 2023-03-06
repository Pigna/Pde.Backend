using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ConnectionService.Contexts;
public interface IDatabaseContext
{
    void Connection();
}