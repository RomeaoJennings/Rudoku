using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rudoku
{
    public enum SudokuSymetry
    {
        None,
        Random,
        Rotational,
        VerticalFlip,
        HorizontalFlip,
        DiagonalFlip,
        ReverseDiagonalFlip,
        HorizontalVerticalFlip
    }
}
