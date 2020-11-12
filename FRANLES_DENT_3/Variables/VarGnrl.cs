using System.Collections.Generic;
using System.Linq;

namespace FRANLES_DENT_3.Variables
{
    static public class VarGnrl
    {
        static private Dictionary<string, string> modulos = new Dictionary<string, string>
        {
            { "Mant_Perfil", "AEMjsxFa5hkCrSdg2tCvfFttphIBJvsBXMteTHEYDC4=" },
            { "Mant_Usuari", "wUgod0IPxcCx5D7I0Qxb2wWAdxYpNeuceuy1zPJYWtM=" },
            { "Root_CliRol", "YhqGOCf2+mrSGddrI0AqTl75kITiBeuW9aQsZ0i39tE=" },
            { "Conf_Empres", "U5DcOYpR8g2TeAL7uXuiDrUldMpNjKbiMEq/bK+m8mQ=" },
            { "Conf_Atribu", "1bj6WOlnNLZQ/Ka5Wixpp6RlgYBZc/QlSy4FUHVfOxo=" }
        };

        static private Dictionary<string, string> acciones = new Dictionary<string, string>
        {
            { "Upd", "557064" },
            { "Add", "416464" },
            { "Del", "44656c" },
            { "Vie", "566965" },
            { "Lst", "4c7374" },
            { "Agr", "416772" },
            { "Mod", "4d6f64" }
        };

        static private Dictionary<string, string> accionesCliente = new Dictionary<string, string>
        {            
            { "Agr", "416772" },
            { "Mod", "4d6f64" }
        };

        static public string AccionModulo(string _key, string _modulo)
        {
            if (string.IsNullOrEmpty(_key) || _key.Length != 50)
            {
                return "notMod";
            }

            string keyacc = _key.Substring(44, 6);
            string keymod = _key.Substring(0, 44);

            if (!modulos.ContainsKey(_modulo))
            {
                return "notMod";
            }

            if (modulos[_modulo] != keymod)
            {
                return "notMod";
            }

            if (!acciones.ContainsValue(keyacc))
            {
                return "notAcc";
            }

            return acciones.FirstOrDefault(s => s.Value.Equals(keyacc)).Key;  
        }

        static public string GetModuloKey(string _modulo)
        {
            if (modulos.ContainsKey(_modulo))
            {
                return modulos[_modulo];
            }

            return null;
        }

        static public string GetActionKey(string _accion)
        {
            if (acciones.ContainsKey(_accion))
            {
                return acciones[_accion];
            }

            return null;
        }

        static public string GetModuloActionKey(string _modulo, string _action)
        {
            string[] modact = new string[2];

            modact[0] = GetModuloKey(_modulo);
            modact[1] = GetActionKey(_action);

            if (modact[0] == null || modact[1] == null)
            {
                return null;
            }

            return modact[0] + modact[1];
        }

        static public Dictionary<string, string> GetActionAll()
        {
            return acciones;
        }

        static public string GetActionClient(string _accion)
        {
            return accionesCliente.ContainsValue(_accion) ? accionesCliente.FirstOrDefault(s => s.Value.Equals(_accion)).Key : "notAccClient";
        }
    }
}