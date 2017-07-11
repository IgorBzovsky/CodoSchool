﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodoSchool.Data.Abstractions
{
    public interface IUnitOfWork
    {
        ISectionsRepository Sections { get; }
        ISectionTypesRepository SectionTypes { get; }
        int Complete();
    }
}
