using System;
using System.Collections.Generic;

namespace FRANLES_DENT_3.Variables
{
    static public class VarGnrl
    {
        static private Dictionary<string, string> modulos = new Dictionary<string, string>
        {
            { "Mant_Perfil", "AEMjsxFa5hkCrSdg2tCvfFttphIBJvsBXMteTHEYDC4=" },
            { "Parm_Depart", "8KJPox2vb2P1yqQzwbldpofdZEMfHU60PAHSccRqDiY=" },
            { "Mant_Usuari", "wUgod0IPxcCx5D7I0Qxb2wWAdxYpNeuceuy1zPJYWtM=" },
            { "Root_CliRol", "YhqGOCf2+mrSGddrI0AqTl75kITiBeuW9aQsZ0i39tE=" },
            { "Conf_Empres", "U5DcOYpR8g2TeAL7uXuiDrUldMpNjKbiMEq/bK+m8mQ=" },
            { "Conf_Atribu", "1bj6WOlnNLZQ/Ka5Wixpp6RlgYBZc/QlSy4FUHVfOxo=" }

        };

        static private Dictionary<string, string> acciones = new Dictionary<string, string>
        {
            { "557064" , "Upd" },
            { "416464" , "Add" },
            { "44656c" , "Del" },
            { "566965" , "Vie" },
            { "4c7374" , "Lst" }
        };

        static public string AccionModulo(string _key, string _modulo)
        {
            try
            {

                string keyacc = _key.Substring(44, 6);
                string keymod = _key.Substring(0, 44);

                if (modulos.ContainsKey(_modulo))
                {
                    if (modulos[_modulo] == keymod)
                    {
                        if (acciones.ContainsKey(keyacc))
                        {
                            return acciones[keyacc];
                        }
                        else
                            return "notAcc";
                    }
                    else
                        return "notMod";
                }
                else
                    return "notMod";
            }
            catch (Exception ex)
            {
                return "notMod";
            }
        }
    }
}