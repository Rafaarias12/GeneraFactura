import { detalles } from "./detalles.model";

export interface factura {
  id?: number;
  cliente: string;
  fecha: Date;
  total: number;
  detalleFacturas: detalles[];
}
