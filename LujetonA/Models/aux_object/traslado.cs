using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LujetonA.Models.aux_object
{
    public class traslado
    {
        private int idBodegaSalida;
        private int idProducto;
        private int cantidad;
        private int idBodegaDestino;

        public int IdBodegaSalida
        {
            get
            {
                return idBodegaSalida;
            }

            set
            {
                idBodegaSalida = value;
            }
        }
        public int IdProducto
        {
            get
            {
                return idProducto;
            }

            set
            {
                idProducto = value;
            }
        }
        public int Cantidad
        {
            get
            {
                return cantidad;
            }

            set
            {
                cantidad = value;
            }
        }
        public int IdBodegaDestino
        {
            get
            {
                return idBodegaDestino;
            }

            set
            {
                idBodegaDestino = value;
            }
        }

    }
}