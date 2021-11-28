using System;

class MapDirectionalLayer
{
    //1 layer for directions
    //1 layer for cost
    //1 node for default
    //position
    //width, height

    DirectionalLayer directionLayer;
    DirectionalLayer addedCountLayer;
    DirectionalNode defaultNode;

    public MapDirectionalLayer(int perSideWidth, int perSideLength, DirectionalNode defaultNode)
    {
        this.defaultNode = defaultNode;
        this.directionLayer = new DirectionalLayer(perSideWidth, perSideLength);
        this.directionLayer.Set(this.defaultNode);

        this.addedCountLayer = new DirectionalLayer(perSideWidth, perSideLength);

    }

    //Delegate callbacks
        //Layer modified
            //coordinates
            //min, max coordinates?
    

    //AddLayerAtPoint, Rotation
    //RemoveLayerAtPoint, Rotation

    //
    public void AddDirectionalLayerAtPoint(DirectionalLayer addingLayer, int centerX, int centerY)
    {
        if (addingLayer == null) { return; }

        //Get indexes to work with.
        int addingLayer_minX = centerX + this.directionLayer.GetSideWidth() - addingLayer.GetSideWidth() ;
        int addingLayer_minY = centerY + this.directionLayer.GetSideLength() - addingLayer.GetSideLength();
        int addingLayer_maxX = centerX + this.directionLayer.GetSideWidth() + addingLayer.GetSideWidth();
        int addingLayer_maxY = centerY + this.directionLayer.GetSideLength() + addingLayer.GetSideLength();

        for (int x = addingLayer_minX; x < addingLayer_maxX; ++x) {
            for (int y = addingLayer_minY; y < addingLayer_maxY; ++y)
            {

            }
        }
    }

    void AddDirectionPointAtPoint(int x, int y, DirectionalNode addingNode)
    {
        if (x >= 0 &&
            x < this.directionLayer.directionalNodes.GetLength(0) &&
            y >= 0 && 
            y < this.directionLayer.directionalNodes.GetLength(1))
        {
            DirectionalNode diffNode = this.directionLayer.directionalNodes[x, y] ^ addingNode;
            //Left off - Cost needs integers.
        }
    }

}