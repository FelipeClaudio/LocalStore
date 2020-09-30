using FluentAssertions;
using LocalStore.Commons.Definitions;
using System;
using Xunit;

namespace LocalStore.UnitTests.LocalStore.Commons
{
    public class EntityBaseTest
    {
        private readonly Guid _entityGuid;
        private readonly EntityBaseFake _entity;

        public EntityBaseTest()
        {
            this._entityGuid = Guid.NewGuid();
            this._entity = new EntityBaseFake(this._entityGuid);
        }

        [Fact(DisplayName = "Feature: EntityBaseFake. | Given: Existent Guid. | When: Instantiate. | Should: Construct object with Id and CreationTime specified.")]
        public void EntityBaseFake_ExistentGuid_ShouldConstructObjectWithIdAndCreationTimeSpecified()
        {
            // Assert
            this._entity.Id.Should().Be(this._entityGuid);
            this._entity.CreationTime.Should().BeCloseTo(DateTime.Now);
        }

        [Fact(DisplayName = "Feature: EntityBaseFake. | Given: Null Guid. | When: Instantiate. | Should: Construct object with new Id.")]
        public void EntityBaseFake_NullGuid_ShouldConstructObjectWithNewId()
        {
            // Arrange / Act
            var entity = new EntityBaseFake(null);

            // Act
            entity.Id.Should().NotBe(this._entityGuid);
            entity.CreationTime.Should().BeCloseTo(DateTime.Now);
        }

        [Fact(DisplayName = "Feature: EntityBaseFake. | Given: Null object compared. | When: Equals. | Should: Return false.")]
        public void Equals_NullObjectCompared_ShouldReturnFalse()
        {
            // Act
            var isEqual = this._entity.Equals(null);

            // Assert
            isEqual.Should().BeFalse();
        }

        [Fact(DisplayName = "Feature: EntityBaseFake. | Given: Object of different type in comparison. | When: Equals. | Should: Return false.")]
        public void Equals_ObjectOfDifferentTypeInComparison_ShouldReturnFalse()
        {
            // Act
            var isEqual = this._entity.Equals(new DateTime());

            // Assert
            isEqual.Should().BeFalse();
        }

        [Fact(DisplayName = "Feature: EntityBaseFake. | Given: Equal objects. | When: Equals. | Should: Return true.")]
        public void Equals_EqualObjects_ShouldReturnTrue()
        {
            // Act
            var newEntity = new EntityBaseFake(this._entityGuid);
            var isEqual = this._entity.Equals(newEntity);

            // Assert
            isEqual.Should().BeTrue();
        }
    }


    public class EntityBaseFake : EntityBase 
    {
        public EntityBaseFake(Guid? id) : base(id) { }
    }
}
