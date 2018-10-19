﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Query.Predicates.Operators
{
    public enum ValueComparingOperator
    {
        None,
        GreaterThan,
        GreaterThanOrEqual,
        Equal,
        NotEqual,
        LessThan,
        LessThanOrEqual,
        StringContains
    }
}
