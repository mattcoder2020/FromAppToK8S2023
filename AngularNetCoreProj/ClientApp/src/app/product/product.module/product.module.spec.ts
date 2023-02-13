import { Productmodule } from './product.module';

describe('Product.ModuleModule', () => {
  let productModuleModule: Productmodule;

  beforeEach(() => {
    productModuleModule = new Productmodule();
  });

  it('should create an instance', () => {
    expect(productModuleModule).toBeTruthy();
  });
});
