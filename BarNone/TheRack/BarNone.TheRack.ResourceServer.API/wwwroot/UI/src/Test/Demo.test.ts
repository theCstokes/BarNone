import * as chai from "chai"

let expect = chai.expect;
let should = chai.should();

var addTwo = function (num1: number, num2: number) {
	return num1 + num2;
};

describe("Test the behavior of addTwo()", function () {
    it('should return 2 when given 1 and 1 via expect()', function () {
        expect(addTwo(1, 1)).to.be.equal(2)
    })
    it('should not return 3 when given 1 and 1 via should()', function () {
        addTwo(1, 1).should.not.be.equal(3)
    })
})