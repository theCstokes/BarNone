import 'mocha';
import * as chai from "chai"
import { FilterBuilder } from "Vee/Core/FilterBuilder"

let expect = chai.expect;


describe("FilterBuilder", () => {
	it("Builds Property Path", () => {
		var h = FilterBuilder.getHeader({
			property: (u: User) => u.parent.id,
			value: null
		});

		expect(h.where[0].property).to.equal("parent.id");
		expect(h.where[0].op).to.equal("eq");
	});
});

class User {
	public parent: { id: number };
}