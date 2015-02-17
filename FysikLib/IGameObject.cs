using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace FysikLib
{
    public interface IGameObject
    {
        void Update(float delta);

        void Draw(SpriteBatch batch);
    }
}
