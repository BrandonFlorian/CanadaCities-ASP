/* Authors: Brandon Florian, Tristan Kornacki, Ryan Fisher
 * File: CityDto.cs
 * Purpose: Enum values for provinces.
 * Date: Feb 16, 2020
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CanadaCities_ASP.Classes
{
    public enum Provinces
    {
        Unselected = -1,
        Alberta = 0,
        BC = 1,
        Saskatchewan = 2,
        Manitoba = 3,
        Ontario = 4,
        Quebec = 5,
        PEI = 6,
        NS = 7,
        NB = 8,
        NFLD = 9,
        Yukon = 10,
        NWT = 11,
        Nunavut = 12,
    }
}