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
    this.service.getFacturas().subscribe(
      data =>{
        console.log(data);
        this.facturas = data;

      }

    );
    console.log(this.facturas);
  }



  openModal(id: number) {
    const modalRef = this.modalService.open(FacturaComponent, { size: 'lg' });
    modalRef.componentInstance.facturaId = id;
  }

}
