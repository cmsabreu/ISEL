﻿using TP2SI2.model;
using System.Collections.Generic;
namespace TP2SI2.mapper.interfaces
{
    interface ISchedulingMapper : IMapper<Scheduling, KeyValuePair<int, int>, List<Scheduling>> { }
}

