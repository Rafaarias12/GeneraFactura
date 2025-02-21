import { CommonModule } from '@angular/common';
import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';
import { factura } from '../../../models/factura.model';
import { FacturaService } from '../../../services/factura.service';

@Component({
  selector: 'app-factura',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './factura.component.html',
  styleUrl: './factura.component.scss',
})
export class FacturaComponent implements OnChanges {
  @Input() factura?: factura;
  @Input() facturaId: number = 0;
  facturaForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    public activeModal: NgbActiveModal,
    private service: FacturaService
  ) {
    this.facturaForm = this.fb.group({
      cliente: ['', Validators.required],
      fecha: ['', Validators.required],
      total: [{ value: 0, disabled: true }],
      detalles: this.fb.array([])
    });
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['facturaId'] && this.facturaId !== 0) {
      this.service.getFacturasId(this.facturaId).subscribe(
        x => {
          console.log('Factura cargada:', x);
          console.log('Detalles:', x.detalleFacturas);

          // Actualizar campos principales
          this.facturaForm.patchValue({
            cliente: x.cliente,
            fecha: new Date(x.fecha).toISOString().split('T')[0],
            total: x.total
          });

          // Limpiar detalles existentes
          while (this.detalles.length) {
            this.detalles.removeAt(0);
          }

          // Agregar detalles
          if (x.detalleFacturas) {
            x.detalleFacturas.forEach(detalle => {
              const detalleForm = this.fb.group({
                Producto: [detalle.producto || ''],
                Cantidad: [detalle.cantidad || 0],
                PrecioUnitario: [detalle.precioUnitario || 0],
                Subtotal: [{ value: detalle.subtotal || 0, disabled: true }]
              });
              this.detalles.push(detalleForm);
            });
          }

          console.log('Form después de cargar:', this.facturaForm.value);
        }
      );
    }
  }

  get detalles() {
    return this.facturaForm.get('detalles') as FormArray;
  }

  agregarDetalle() {
    const detalleForm = this.fb.group({
      Producto: ['', Validators.required],
      Cantidad: [1, [Validators.required, Validators.min(1)]],
      PrecioUnitario: [0, [Validators.required, Validators.min(0)]],
      Subtotal: [{ value: 0, disabled: true }]
    });

    detalleForm.valueChanges.subscribe(() => {
      this.actualizarSubtotal(detalleForm);
    });

    this.detalles.push(detalleForm);
    this.actualizarTotal();
  }

  actualizarSubtotal(detalleForm: FormGroup) {
    const cantidad = detalleForm.get('Cantidad')?.value || 0;
    const precio = detalleForm.get('PrecioUnitario')?.value || 0;
    const subtotal = cantidad * precio;
    detalleForm.patchValue({ Subtotal: subtotal }, { emitEvent: false });

    this.actualizarTotal();
  }

  actualizarTotal() {
    const total = this.detalles.controls.reduce((acc, detalle) => {
      return acc + (detalle.get('Subtotal')?.value || 0);
    }, 0);

    this.facturaForm.patchValue({ total }, { emitEvent: false });
  }

  removeDetalle(index: number) {
    this.detalles.removeAt(index);
    this.actualizarTotal(); // Recalcula el total al eliminar un detalle
  }

  guardar() {
    if (this.facturaForm.valid) {
      const facturaData: factura = {
        cliente: this.facturaForm.get('cliente')?.value,
        fecha: this.facturaForm.get('fecha')?.value,
        total: this.facturaForm.get('total')?.value,
        detalleFacturas: this.detalles.controls.map(control => ({
          factura: this.facturaId,
          producto: control.get('Producto')?.value,
          cantidad: control.get('Cantidad')?.value,
          precioUnitario: control.get('PrecioUnitario')?.value,
          subtotal: control.get('Subtotal')?.value
        }))
      };

      if (this.facturaId === 0) {
        this.service.addFactura(facturaData).subscribe({
          next: (response) => {
            console.log('Factura guardada:', response);
            this.activeModal.close(true);
          },
          error: (error) => console.error('Error al guardar:', error)
        });
      }
      else{
        this.service.putFactura(this.facturaId, facturaData).subscribe({
          next: (response) => {
            console.log('Factura guardada:', response);
            this.activeModal.close(true);
          },
          error: (error) => console.error('Error al guardar:', error)
        })
      }
    }
  }
}
