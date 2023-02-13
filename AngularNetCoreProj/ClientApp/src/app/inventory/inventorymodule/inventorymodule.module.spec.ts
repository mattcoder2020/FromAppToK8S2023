import { InventorymoduleModule } from './inventorymodule.module';

describe('InventorymoduleModule', () => {
  let inventorymoduleModule: InventorymoduleModule;

  beforeEach(() => {
    inventorymoduleModule = new InventorymoduleModule();
  });

  it('should create an instance', () => {
    expect(inventorymoduleModule).toBeTruthy();
  });
});
