using System.Collections;
using student;

class GroupEnumerator : IEnumerator<Student>
{
    List<Student> collection;
    int index = 0;

    public GroupEnumerator(List<Student> collection)
    {
        this.collection = collection;
    }


    public Student Current
    {
        get
        {
            if (index < 0 || index >= collection.Count)
                {
                    throw new InvalidOperationException("Enumerator is in an invalid state.");
                }
            return collection[index++];
        }
        
    }

    object IEnumerator.Current => Current;

    public bool MoveNext()
    {
        if(index < collection.Count)
        {
            return true;
        }
        return false;
    }

    public void Reset()
    {
        index = 0;
    }

    public void Dispose(){}
}