using Flashcards.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Interfaces
{
    interface ISource
    {
        FlashItem GetWord();
    }
}
