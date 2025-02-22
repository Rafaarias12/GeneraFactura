import { Component, inject } from '@angular/core';
import { factura } from '../models/factura.model';
import { FacturaService } from '../services/factura.service';
import { FacturaComponent } from './modal/factura/factura.component';
import { NgbModal, NgbModule } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-facturas',
  standalone: true,
  imports: [NgbModule],
  templateUrl: './facturas.component.html',
  styleUrl: './facturas.component.scss'
})
export default class FacturasComponent {

  private modalService = inject(NgbModal);
  facturas: factura[] = [];

  constructor(private service: FacturaService){

  }

  ngOnInit(){
    this.cargarFacturas(); // Cargar al inicio
  }

  cargarFacturas(){
    this.service.getFacturas().subscribe(
      data =>{
        this.facturas = data;
      }
    );
  }


  openModal(id: number) {
    const modalRef = this.modalService.open(FacturaComponent, { size: 'lg' });
    modalRef.componentInstance.facturaId = id;
  }

  deleteFactura(id: number){
    this.service.deleteFactura(id).subscribe(
      x =>{
        console.log(x);
      }
    )
  }

}
