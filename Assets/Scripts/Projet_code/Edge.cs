
   public class Edge{
        public Cell cell1; // Cellule d'origine
        public Cell cell2; // Cellule d'arrivée
        public double weight;
        // Constructor 
        public Edge(Cell cell1, Cell cell2, double weight=1){
            this.cell1 = cell1;
            this.cell2 = cell2;
            this.weight = weight;

        }
        public void EdgeInitialisation(Cell cell1, Cell cell2){
            this.cell1 = cell1;
            this.cell2 = cell2;
            this.weight = 1;
        }
        public void modifyWeight(int weight){
            this.weight = weight;
        }



    }

