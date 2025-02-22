import { detalles } from "./detalles.model";

export interface factura {
  id?: number;
  cliente: string;
  fecha: string;
  total: number;
  detalleFacturas: detalles[];
}
