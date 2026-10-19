global using Qowaiv;
global using Qowaiv.Diagnostics.Contracts;
global using Qowaiv.Validation.DataAnnotations;
global using System;
global using System.Collections;
global using System.Collections.Concurrent;
global using System.Collections.Generic;
global using System.ComponentModel;
global using System.ComponentModel.DataAnnotations;
global using System.Diagnostics;
global using System.Diagnostics.CodeAnalysis;
global using System.Diagnostics.Contracts;
global using System.Globalization;
global using System.Linq;
global using System.Runtime.CompilerServices;
#if NETSTANDARD2_0
global using Lock = System.Object;
#else
global using Lock = System.Threading.Lock;
#endif
